using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoConfiguration;
using Minio;


namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

public class MinioGetMapsNamesQuery : IGetMapsNamesQuery
{
    private readonly MinioClient _minioClient;
    private readonly Configuration _configuration;

    public MinioGetMapsNamesQuery(IMinioClientBuilder minioClientBuilder, Configuration configuration)
    {
        _minioClient = minioClientBuilder.Build();
        _configuration = configuration;
    }
    public async Task<MapNamesResultModel> GetMapsNamesAsync()
    {
        try
        {
            List<string> mapsNames = new();
            var listArgs = new ListObjectsArgs()
                        .WithBucket(_configuration.MapsBucket);

            var observable = _minioClient.ListObjectsAsync(listArgs);
            var subscription = observable.Subscribe(
                item => mapsNames.Add(item.Key)
            );

            var result = new MapNamesResultModel
            {
                Success = true,
                MapsNames = mapsNames,
                ErrorMessage = ""
            };

            return result;
        }
        catch (Exception)
        {
            var result = new MapNamesResultModel
            {
                Success = false,
                MapsNames = new List<string>(),
                ErrorMessage = "Get Maps Names failed"
            };
            return result;
        }
    }
}

