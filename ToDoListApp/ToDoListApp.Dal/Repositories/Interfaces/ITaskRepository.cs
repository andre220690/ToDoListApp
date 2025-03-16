using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Models;

namespace ToDoListApp.Dal.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto> GetByIdAsync(long id);
    Task<TaskDto> CreateAsync(TaskDto taskDto);
    Task<TaskDto> UpdateAsync(TaskDto taskDto);
    Task DeleteAsync(long id);
    Task<bool> ExistsAsync(long id);
}