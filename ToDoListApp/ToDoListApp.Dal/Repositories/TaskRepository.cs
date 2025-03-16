using Microsoft.EntityFrameworkCore;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Extensions;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Dal.Repositories;

internal class TaskRepository : BaseRepository, ITaskRepository
{
    public TaskRepository(TasksDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        var tasksDb = await _dbContext.Tasks.Include(r => r.TaskStatus).ToArrayAsync();

        return tasksDb.Select(r => r.ToDomain()).ToArray();
    }

    public async Task<TaskDto> GetByIdAsync(long id)
    {
        var taskDb = await _dbContext.Tasks.FindAsync(id);

        return taskDb?.ToDomain();
    }

    public async Task<TaskDto> CreateAsync(TaskDto taskDto)
    {
        var taskDb = taskDto.ToDb();

        await _dbContext.AddAsync(taskDb);
        await _dbContext.SaveChangesAsync();

        return taskDb.ToDomain();
    }

    public async Task<TaskDto> UpdateAsync(TaskDto taskDto)
    {
        var taskDb = taskDto.ToDb();

        await _dbContext.Tasks
            .Where(t => t.Id == taskDb.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Text, taskDb.Text)
                .SetProperty(t => t.TaskStatusId, taskDb.TaskStatusId));

        //await _dbContext.SaveChangesAsync(); // TODO: проверить если задачи такое нет

        return taskDb.ToDomain();
    }

    public async Task DeleteAsync(long id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task != null)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(long id)
    {
        var taskDb = await _dbContext.Tasks.FindAsync(id);
        return taskDb != null;
    }
}
