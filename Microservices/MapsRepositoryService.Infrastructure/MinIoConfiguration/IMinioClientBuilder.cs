using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoConfiguration;

public interface IMinioClientBuilder
{
    MinioClient Build();
}
