using ToDoListApp.Core.Models;

namespace ToDoListApp.Domain.Services.Interfaces;

public interface ITaskDataService
{
    Task<IEnumerable<TaskDto>> GetAllTaskAsync();
    Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
    Task<TaskDto> UpdateTaskAsync(TaskDto taskDto);
    Task DeleteTaskAsync(long taskId);
}
