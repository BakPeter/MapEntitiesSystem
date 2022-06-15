using MessageBroker.Core;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;

namespace NotificationsService.Services;

internal class WorkerService : BackgroundService
{
    private readonly ISubscriber _subscriber;
    private readonly IMissionMapChangedCallbackCommand _missionMapChangedCallbackCommand;
    private readonly IMapEntitySendCallbackCommand _mapEntitySendCallbackCommand;
    private readonly Settings _settings;

    public WorkerService(ISubscriber subscriber,
                         IMissionMapChangedCallbackCommand missionMapChangedCallbackCommand,
                         IMapEntitySendCallbackCommand mapEntitySendCallbackCommand,
                         Settings settings)
    {
        _subscriber = subscriber;
        _missionMapChangedCallbackCommand = missionMapChangedCallbackCommand;
        _mapEntitySendCallbackCommand = mapEntitySendCallbackCommand;
        _settings = settings;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe(_settings.MissionMapTopic, _missionMapChangedCallbackCommand.MissionMapChanged, stoppingToken);
        _subscriber.Subscribe(_settings.EntitiesTopic, _mapEntitySendCallbackCommand.EntityPublished, stoppingToken);
        return Task.CompletedTask;
    }
}