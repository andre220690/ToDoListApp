using Microsoft.AspNetCore.SignalR;

namespace ToDoListApp.Domain.Hubs;

public class TaskHub: Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
