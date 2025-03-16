using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Dal.Repositories;
internal class TaskStatusRepository : BaseRepository, ITaskStatusRepository
{
    public TaskStatusRepository(TasksDbContext dbContext) : base(dbContext) { }

    public async Task<bool> ExistsAsync(long id)
    {
        var taskStatus = await _dbContext.TaskStatuses.FindAsync(id);
        return taskStatus != null;
    }
}
