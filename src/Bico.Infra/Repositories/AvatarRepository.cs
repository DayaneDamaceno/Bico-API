using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Bico.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Bico.Infra.Repositories;

public class AvatarRepository : IAvatarRepository
{
    private readonly IConfiguration _configuration;
    private readonly BlobServiceClient _blobServiceClient;

    public AvatarRepository(IConfiguration configuration, BlobServiceClient blobServiceClient)
    {
        _configuration = configuration;
        _blobServiceClient = blobServiceClient;
    }

    public string GerarAvatarUrlSegura(string blobName, string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b", // 'b' para Blob
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1), // SAS expira em 1 hora
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        string sasToken = ObterSASToken(sasBuilder);

        return $"{blobClient.Uri}?{sasToken}";
    }


    private string ObterSASToken(BlobSasBuilder sasBuilder)
    {
        var keyCredential = new StorageSharedKeyCredential(_blobServiceClient.AccountName, _configuration["KeyBlobStorage"]);
        var sasToken = sasBuilder.ToSasQueryParameters(keyCredential).ToString();
        return sasToken;
    }
}
