namespace ToDoListApp.Dal.Repositories;

internal class BaseRepository
{
    protected TasksDbContext _dbContext;

    public BaseRepository(TasksDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
