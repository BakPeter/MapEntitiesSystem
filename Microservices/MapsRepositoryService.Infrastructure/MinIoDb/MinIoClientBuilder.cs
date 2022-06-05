using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoDb;

internal class MinIoClientBuilder
{
    private readonly ILogger<MinIoConfiguration> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoClientBuilder(ILogger<MinIoConfiguration> logger, MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoConfiguration = minIoConfiguration;
    }


    public MinioClient Build()
    {
        try
        {
            var minIoClient = new MinioClient()
                .WithEndpoint(_minIoConfiguration.Server)
                .WithCredentials(_minIoConfiguration.User, _minIoConfiguration.Password)
                .Build();

            if (minIoClient == null)
            {
                throw new InvalidOperationException("minIoClient is null");
            }

            CreateBucketIfNotExists(minIoClient, _minIoConfiguration.MapsBucket).GetAwaiter().GetResult();

            return minIoClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: ex.Message);

            throw new InvalidOperationException("minIoClient creation failed");
        }
    }

    private async Task CreateBucketIfNotExists(IBucketOperations minIo, string bucketName)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await minIo.BucketExistsAsync(bucketExistsArgs);
            if (!found)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                await minIo.MakeBucketAsync(makeBucketArgs);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new InvalidOperationException("minIo bucket creation failed");
        }
    }
}