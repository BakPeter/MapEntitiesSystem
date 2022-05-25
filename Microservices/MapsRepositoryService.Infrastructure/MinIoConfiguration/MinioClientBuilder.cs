using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsRepositoryService.Infrastructure.MinIoConfiguration
{
    internal class MinioClientBuilder : IMinioClientBuilder
    {
        private readonly Configuration _configuration;

        public MinioClientBuilder(Configuration configuration)
        {
            _configuration = configuration;
        }
        public MinioClient Build()
        {
            try
            {
                MinioClient minioClient = new MinioClient()
                .WithEndpoint(_configuration.Server)
                .WithCredentials(_configuration.User, _configuration.Password)
                .Build();

                if (minioClient == null)
                {
                    throw new InvalidOperationException("minioClient is null");
                }

                CreateBucketIfNotExists(minioClient, _configuration.MapsBucket).GetAwaiter().GetResult();

                return minioClient;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("minioClient creation failed");
            }
        }

        private static async Task CreateBucketIfNotExists(MinioClient minio, string bucketName)
        {
            try
            {
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
                var found = await minio.BucketExistsAsync(bucketExistsArgs);
                if (!found)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minio.MakeBucketAsync(makeBucketArgs);
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("minio bucket creation failed");
            }
        }
    }
}
