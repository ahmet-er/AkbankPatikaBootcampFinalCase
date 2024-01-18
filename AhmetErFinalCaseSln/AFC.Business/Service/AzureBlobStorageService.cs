using AFC.Schema;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

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
        var containerClient = blobServiceClient.GetBlobContainerClient("expense_files"); // containerName
        var blobClient = containerClient.GetBlobClient($"{DateTime.Now.Ticks}_{file.FileName}");

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return new ExpenseDocumentResponse
        {
            FileName = file.Name,
            FileType = file.ContentType,
            FilePath = blobClient.Uri.ToString(),
        };
    }
}
