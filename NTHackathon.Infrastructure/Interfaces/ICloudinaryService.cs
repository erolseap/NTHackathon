using Microsoft.AspNetCore.Http;

namespace NTHackathon.Infrastructure.Interfaces;

public interface ICloudinaryService
{
    Task<string> FileCreateAsync(IFormFile file);
    Task<bool> FileDeleteAsync(string filePath);
}
