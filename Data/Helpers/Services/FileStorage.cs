using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Data.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Data.Helpers.Services;

public class FileStorage : IFileStorage
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public FileStorage(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("AzureStorage")!;
    }

    public async Task RemoveFileAsync(string filePath, string containerName)
    {
        var client = new BlobContainerClient(_connectionString, containerName);
        await client.CreateIfNotExistsAsync();

        var fileName = Path.GetFileName(filePath);
        var blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
    {
        var client = new BlobContainerClient(_connectionString, containerName);
        await client.CreateIfNotExistsAsync();

        client.SetAccessPolicy(PublicAccessType.Blob);

        var fileName = $"{Guid.NewGuid()}.{extention}";
        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms);
        }

        return blob.Uri.ToString();
    }
}