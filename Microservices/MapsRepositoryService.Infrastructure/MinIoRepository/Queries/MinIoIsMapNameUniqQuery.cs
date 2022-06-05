using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

internal class MinIoIsMapNameUniqQuery : IIsMapNameUniqQuery
{
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoIsMapNameUniqQuery(MinIoClientBuilder minIoClientBuilder, MinIoConfiguration minIoConfiguration)
    {
        _minIoClient  = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<IsMapNameUniqResultModel> IsMapNameUniq(string mapName)
    {
        try
        {
            var args = new StatObjectArgs()
            .WithBucket(_minIoConfiguration.MapsBucket)
            .WithObject(mapName);

            var result = await _minIoClient.StatObjectAsync(args);


        }
        catch (Exception ex)
        {

            throw;
        }

        throw new NotImplementedException();
    }
}
