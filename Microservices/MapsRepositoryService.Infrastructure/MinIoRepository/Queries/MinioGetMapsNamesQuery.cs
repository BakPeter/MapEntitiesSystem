using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

internal class MinIoGetMapsNamesQuery : IGetMapsNamesQuery
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoGetMapsNamesQuery> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoGetMapsNamesQuery(
         ILogger<MinIoGetMapsNamesQuery> logger,
         MinIoClientBuilder minIoClientBuilder, 
         MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public Task<MapNamesResultModel> GetMapsNamesAsync()
    {
        try
        {
            List<string> mapsNames = new();
            var listArgs = new ListObjectsArgs()
                        .WithBucket(_minIoConfiguration.MapsBucket);

            var observable =  _minIoClient.ListObjectsAsync(listArgs);
            _ = observable.Subscribe(
                item => mapsNames.Add(item.Key)
            );

            var result = new MapNamesResultModel
            {
                Success = true,
                MapsNames = mapsNames,
                ErrorMessage = ""
            };

           return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var result = new MapNamesResultModel
            {
                Success = false,
                MapsNames = new List<string>(),
                ErrorMessage = "Get Maps Names failed"
            };
            return Task.FromResult(result);
        }
    }
}