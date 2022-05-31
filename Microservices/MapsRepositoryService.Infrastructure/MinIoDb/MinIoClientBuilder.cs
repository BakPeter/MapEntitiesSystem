using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoDb;

internal class MinIoClientBuilder
{
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoClientBuilder(MinIoConfiguration minIoConfiguration)
    {
        _minIoConfiguration = minIoConfiguration;
    }

    public MinioClient Build()
    {
        var minIoClient = new MinioClient()
            .WithEndpoint(_minIoConfiguration.Server)
            .WithCredentials(_minIoConfiguration.User, _minIoConfiguration.Password)
            .Build();
        try
        {
            if (minIoClient == null)
            {
                throw new InvalidOperationException("minIoClient is null");
            }

            CreateBucketIfNotExists(minIoClient, _minIoConfiguration.MapsBucket).GetAwaiter().GetResult();

            return minIoClient;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("minIoClient creation failed");
        }
    }

    private static async Task CreateBucketIfNotExists(IBucketOperations minIo, string bucketName)
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
        catch (Exception)
        {
            throw new InvalidOperationException("minIo bucket creation failed");
        }
    }
}