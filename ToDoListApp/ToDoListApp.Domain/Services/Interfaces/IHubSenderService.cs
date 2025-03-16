using ToDoListApp.Core.Models;

namespace ToDoListApp.Domain.Services.Interfaces;
public interface IHubSenderService
{
    Task InvokeAddTaskAsync(TaskDto taskDto);
    Task InvokeUpdateTaskAsync(TaskDto tasDto);
    Task InvokeDeleteTaskAsync(long taskId);
}
