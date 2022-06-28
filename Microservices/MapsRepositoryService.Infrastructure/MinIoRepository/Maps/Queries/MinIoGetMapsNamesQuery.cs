using System.Reactive.Linq;
using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;

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

    public async Task<MapNamesResultModel> GetMapsNamesAsync()
    {
        try
        {
            var listArgs = new ListObjectsArgs()
                        .WithBucket(_minIoConfiguration.MapsBucket).WithRecursive(true);
            var files = await _minIoClient.ListObjectsAsync(listArgs).ToList();

            var result = new MapNamesResultModel
            {
                Success = true,
                MapsNames = files.Select(f => f.Key).ToList(),
                ErrorMessage = ""
            };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{ErrorMessage}", ex.Message);

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