using MapEntitiesService.Core.DTO;
using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;

namespace MapEntitiesService.Core.Services
{
    public class MapEntityService : IMapEntityService
    {
        public async Task<HandleEntityResponse> HandleEntityAsync(HandleEntityRequest handleEntityRequestDto)
        {
            return await Task.Run(() =>
            {
                var response = new HandleEntityResponse { HandleSuccess = false };

                try
                {
                    //var logger =  Task.Run(()=>_loggerService.Log(msg));
                    //var publish =  _publishService.Publish(msg);

                    var logger = Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        return true;
                    });
                    var publish = Task.Run(() => true);
                    Task.WaitAll(logger, publish);

                    var result = logger.Result && publish.Result;

                    response.HandleSuccess = result;
                    response.Message = result ? "Operation successful" : "Operation failed";
                }
                catch (Exception e)
                {
                    response.Message = e.ToString();
                }

                return response;
            });

        }

        public async Task<Entity[]> GetAllEntitiesAsync()
        {
            var result = await Task.Run(() =>
             {
                 var entities = new Entity[]
                 {
                    new() {Lat = 10,Lon = 20},
                    new()  {Lat = 20,Lon = 40}
                 };
                 return entities;
             });

            return result;
        }
    }
}
