using ToDoListApp.Auth.Models;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Auth.Services;

public interface IAuthService
{
    Task<AuthResponse> CreateUserAsync(User user);
    Task<AuthResponse> LoginAsync(User user);
}
