using MessageBroker.Core;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;

namespace NotificationsService.Services;

internal class WorkerService : BackgroundService
{
    private readonly ISubscriber _subscriber;
    private readonly IMissionMapChangedCallbackCommand _missionMapChangedCallbackCommand;
    private readonly Settings _settings;

    public WorkerService(ISubscriber subscriber,
                         IMissionMapChangedCallbackCommand missionMapChangedCallbackCommand,
                         Settings settings)
    {
        _subscriber = subscriber;
        _missionMapChangedCallbackCommand = missionMapChangedCallbackCommand;
        _settings = settings;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe(_settings.Topic, _missionMapChangedCallbackCommand.MissionMapChanged, stoppingToken);
        return Task.CompletedTask;
    }
}