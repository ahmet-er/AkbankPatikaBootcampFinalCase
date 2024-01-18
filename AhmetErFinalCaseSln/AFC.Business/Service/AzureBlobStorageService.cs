using AFC.Schema;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace AFC.Business.Service;

public interface IAzureBlobStorageService
{
    Task<ExpenseDocumentResponse> UploadFileAsync(IFormFile file);
}

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient;
    //private readonly string containerName;
    public AzureBlobStorageService(IConfiguration configuration) //, string containerName
    {
        blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureStorageConnection"));
        //this.containerName = containerName;
    }

    public async Task<ExpenseDocumentResponse> UploadFileAsync(IFormFile file)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient("expensefiles"); // containerName

        var sanitizedFileName = SanitizeBlobName($"{DateTime.Now.Ticks}{file.FileName}");

        var blobClient = containerClient.GetBlobClient(sanitizedFileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return new ExpenseDocumentResponse
        {
            FileName = sanitizedFileName,
            FileType = file.ContentType,
            FilePath = blobClient.Uri.ToString(),
        };
    }
    private string SanitizeBlobName(string fileName)
    {
        var sanitizedName = Regex.Replace(fileName, @"[^\w\d\._-]", "_");
        return sanitizedName.ToLower();
    }
}
