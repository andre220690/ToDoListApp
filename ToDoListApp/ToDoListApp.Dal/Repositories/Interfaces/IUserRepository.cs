using ToDoListApp.Core.Models;

namespace ToDoListApp.Dal.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<bool> ExistsAsync(User user);
}
