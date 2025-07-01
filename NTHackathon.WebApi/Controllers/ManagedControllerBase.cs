using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NTHackathon.WebApi.Controllers;

public abstract class ManagedControllerBase : ControllerBase, IAsyncActionFilter
{
    [NonAction]
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cancellationToken = context.HttpContext.RequestAborted;
        if (await OnActionExecutionAsync(context, cancellationToken))
        {
            var executedContext = await next();
            await OnActionExecutedAsync(executedContext);
        }
    }

    [NonAction]
    protected virtual Task<bool> OnActionExecutionAsync(ActionExecutingContext context, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    [NonAction]
    protected virtual Task OnActionExecutedAsync(ActionExecutedContext context)
    {
        return Task.CompletedTask;
    }
}
