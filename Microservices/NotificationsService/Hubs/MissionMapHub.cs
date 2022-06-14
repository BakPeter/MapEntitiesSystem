using Microsoft.AspNetCore.SignalR;

namespace NotificationsService.Hubs;

public class MissionMapHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}