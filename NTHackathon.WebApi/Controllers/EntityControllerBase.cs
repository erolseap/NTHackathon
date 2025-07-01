using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NTHackathon.Application.Specifications.Shared;
using NTHackathon.Domain.Models;
using NTHackathon.Domain.Repositories;
using NTHackathon.Domain.Specifications;

namespace NTHackathon.WebApi.Controllers;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class WithEntityIncludeAttribute<TEntity> : Attribute
    where TEntity : class, IBaseEntity
{
    public string IncludeString { get; }

    public WithEntityIncludeAttribute(string includeString)
    {
        IncludeString = includeString;
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class TrackedEntityAttribute<TEntity> : Attribute
    where TEntity : class, IBaseEntity
{
}

[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public abstract class EntityControllerBase<TEntity> : ManagedControllerBase
    where TEntity : class, IBaseEntity
{
    private TEntity? _currentEntity = null;

    protected TEntity Entity => _currentEntity ??
                                        throw new InvalidOperationException(
                                            $"{typeof(TEntity).Name} is not loaded. Probably tried to access outside of the controllers action");

    private IReadOnlyRepositoryAsync<TEntity> ReadOnlyRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TEntity>>();
    private IWriteRepositoryAsync<TEntity> WriteRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TEntity>>();

    [NonAction]
    protected override async Task<bool> OnActionExecutionAsync(ActionExecutingContext context, CancellationToken cancellationToken)
    {
        var previous = await base.OnActionExecutionAsync(context, cancellationToken);
        if (!previous)
        {
            return previous;
        }

        if (!context.RouteData.Values.TryGetValue("id", out var rawId) || 
            rawId is not string idString || 
            !int.TryParse(idString, out var id))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TEntity).Name} provided: '{rawId}'");
            return false;
        }

        var entityRepository = !IsTrackedEntityRequested() ? ReadOnlyRepository : WriteRepository;
        _currentEntity = await entityRepository.SingleOrDefaultAsync(GetEntityFetchSpecification(id), cancellationToken);
        if (_currentEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TEntity).Name} with id '{id}' was not found");
            return false;
        }
        return true;
    }

    [NonAction]
    protected virtual ISpecification<TEntity> GetEntityFetchSpecification(int id)
    {
        var specification = new GetEntityByIdSpecification<TEntity>(id);
        {
            var endpoint = ControllerContext.HttpContext.GetEndpoint();
            if (endpoint != null)
            {
                var includeAttributes = endpoint
                    .Metadata
                    .Where(m => m is WithEntityIncludeAttribute<TEntity>)
                    .Cast<WithEntityIncludeAttribute<TEntity>>();

                foreach (var includeAttr in includeAttributes)
                {
                    specification.AddInclude(includeAttr.IncludeString);
                }
            }
        }
        return specification;
    }

    [NonAction]
    private bool IsTrackedEntityRequested()
    {
        var endpoint = HttpContext.GetEndpoint();
        if (endpoint == null)
            return false;

        // Look for the attribute (non-generic search, because of runtime generic type issues)
        var trackedAttr = endpoint.Metadata
            .FirstOrDefault(attr =>
                attr is TrackedEntityAttribute<TEntity>) as TrackedEntityAttribute<TEntity>;

        return trackedAttr != null;
    }
}

[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public abstract class EntityControllerBase<TParentEntity, TEntity> : ManagedControllerBase
    where TParentEntity : class, IBaseEntity
    where TEntity : class, IBaseEntity
{
    private TParentEntity? _parentEntity = null;
    private TEntity? _childEntity = null;

    protected TParentEntity ParentEntity => _parentEntity ??
                                            throw new InvalidOperationException(
                                                $"{typeof(TParentEntity).Name} is not loaded. Probably tried to access outside of the controllers action");
    
    protected TEntity Entity => _childEntity ??
                                     throw new InvalidOperationException(
                                         $"{typeof(TEntity).Name} is not loaded. Probably tried to access outside of the controllers action");

    private IReadOnlyRepositoryAsync<TParentEntity> ReadOnlyParentRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TParentEntity>>();
    private IWriteRepositoryAsync<TParentEntity> WriteParentRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TParentEntity>>();
    
    private IReadOnlyRepositoryAsync<TEntity> ReadOnlyRepository => HttpContext.RequestServices.GetRequiredService<IReadOnlyRepositoryAsync<TEntity>>();
    private IWriteRepositoryAsync<TEntity> WriteRepository => HttpContext.RequestServices.GetRequiredService<IWriteRepositoryAsync<TEntity>>();
    
    
    [NonAction]
    protected abstract void ResolveEntitiesReference(TEntity entity, TParentEntity parentEntity, out IActionResult? result);

    [NonAction]
    protected override async Task<bool> OnActionExecutionAsync(ActionExecutingContext context, CancellationToken cancellationToken)
    {
        var previous = await base.OnActionExecutionAsync(context, cancellationToken);
        if (!previous)
        {
            return previous;
        }

        if (!context.RouteData.Values.TryGetValue("parentid", out var rawParentId) || 
            rawParentId is not string parentIdString || 
            !int.TryParse(parentIdString, out var parentId))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TParentEntity).Name} provided: '{rawParentId}'");
            return false;
        }
        
        if (!context.RouteData.Values.TryGetValue("id", out var rawChildId) || 
            rawChildId is not string idChildString || 
            !int.TryParse(idChildString, out var childId))
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"An invalid id of {typeof(TEntity).Name} provided: '{rawChildId}'");
            return false;
        }

        var parentRepository = !IsTrackedEntityRequested<TParentEntity>() ? ReadOnlyParentRepository : WriteParentRepository;
        _parentEntity = await parentRepository.SingleOrDefaultAsync(GetParentEntityFetchSpecification(parentId), cancellationToken);
        if (_parentEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TParentEntity).Name} with id '{parentId}' was not found");
            return false;
        }

        var childRepository = !IsTrackedEntityRequested<TEntity>() ? ReadOnlyRepository : WriteRepository;
        _childEntity = await childRepository.SingleOrDefaultAsync(GetEntityFetchSpecification(childId), cancellationToken);
        if (_childEntity == null)
        {
            context.Result = Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"A {typeof(TEntity).Name} with id '{childId}' was not found");
            return false;
        }

        ResolveEntitiesReference(_childEntity, _parentEntity, out var actionResult);
        if (actionResult != null)
        {
            context.Result = actionResult;
            return false;
        }

        return true;
    }

    [NonAction]
    protected virtual ISpecification<TParentEntity> GetParentEntityFetchSpecification(int id)
    {
        var specification = new GetEntityByIdSpecification<TParentEntity>(id);
        {
            var endpoint = ControllerContext.HttpContext.GetEndpoint();
            if (endpoint != null)
            {
                var includeAttributes = endpoint
                    .Metadata
                    .Where(m => m is WithEntityIncludeAttribute<TParentEntity>)
                    .Cast<WithEntityIncludeAttribute<TParentEntity>>();

                foreach (var includeAttr in includeAttributes)
                {
                    specification.AddInclude(includeAttr.IncludeString);
                }
            }
        }
        return specification;
    }
    
    [NonAction]
    protected virtual ISpecification<TEntity> GetEntityFetchSpecification(int id)
    {
        var specification = new GetEntityByIdSpecification<TEntity>(id);
        {
            var endpoint = ControllerContext.HttpContext.GetEndpoint();
            if (endpoint != null)
            {
                var includeAttributes = endpoint
                    .Metadata
                    .Where(m => m is WithEntityIncludeAttribute<TEntity>)
                    .Cast<WithEntityIncludeAttribute<TEntity>>();

                foreach (var includeAttr in includeAttributes)
                {
                    specification.AddInclude(includeAttr.IncludeString);
                }
            }
        }
        return specification;
    }
    
    [NonAction]
    private bool IsTrackedEntityRequested<TCheckingEntity>()
        where TCheckingEntity : class, IBaseEntity
    {
        var endpoint = HttpContext.GetEndpoint();
        if (endpoint == null)
            return false;

        // Look for the attribute (non-generic search, because of runtime generic type issues)
        var trackedAttr = endpoint.Metadata
            .FirstOrDefault(attr =>
                attr is TrackedEntityAttribute<TCheckingEntity>) as TrackedEntityAttribute<TCheckingEntity>;

        return trackedAttr != null;
    }
}
