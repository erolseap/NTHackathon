using Microsoft.AspNetCore.Http;

namespace NTHackathon.Infrastructure.İnterfaces;

public interface ICloudinaryService
{
    Task<string> FileCreateAsync(IFormFile file);
    Task<bool> FileDeleteAsync(string filePath);
}
