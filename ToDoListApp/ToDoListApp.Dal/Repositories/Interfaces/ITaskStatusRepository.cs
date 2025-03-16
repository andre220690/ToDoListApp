namespace ToDoListApp.Dal.Repositories.Interfaces;
public interface ITaskStatusRepository
{
    Task<bool> ExistsAsync(long id);
}
