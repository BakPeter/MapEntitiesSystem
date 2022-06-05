using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Minio;
using Minio.Exceptions;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

internal class MinIoIsMapNameUniqQuery : IIsMapNameUniqQuery
{
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoIsMapNameUniqQuery(MinIoClientBuilder minIoClientBuilder, MinIoConfiguration minIoConfiguration)
    {
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<IsMapNameUniqResultModel> IsMapNameUniq(MapNameModel mapNameModel)
    {
        try
        {
            var args = new StatObjectArgs()
            .WithBucket(_minIoConfiguration.MapsBucket)
            .WithObject(mapNameModel.mapName + mapNameModel.mapExtension);

            var result = await _minIoClient.StatObjectAsync(args);

            return new IsMapNameUniqResultModel(Success: true, NameUniq: false);

        }
        catch (ObjectNotFoundException ex)
        {
            return new IsMapNameUniqResultModel(Success: true, NameUniq: true);
        }
        catch (Exception ex)
        {

            return new IsMapNameUniqResultModel(Success: false, ErrorMessage: ex.Message);
        }
    }
}
