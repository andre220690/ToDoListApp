using Microsoft.AspNetCore.SignalR;
using ToDoListApp.Core.Models;
using ToDoListApp.Domain.Hubs;
using ToDoListApp.Domain.Services.Interfaces;

namespace ToDoListApp.Domain.Services;

internal class HubSenderService : IHubSenderService
{
    private readonly IHubContext<TaskHub> _hubContext;

    public HubSenderService(IHubContext<TaskHub> hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    public async Task InvokeAddTaskAsync(TaskDto taskDto)
    {
        await _hubContext.Clients.All.SendAsync("invokeAdd", taskDto);
    }

    public async Task InvokeDeleteTaskAsync(long taskId)
    {
        await _hubContext.Clients.All.SendAsync("invokeDelete", taskId);
    }

    public async Task InvokeUpdateTaskAsync(TaskDto tasDto)
    {
        await _hubContext.Clients.All.SendAsync("invokeUpdate", tasDto);
    }
}
