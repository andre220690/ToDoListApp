using Microsoft.EntityFrameworkCore;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Extensions;
using ToDoListApp.Dal.Models;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Dal.Repositories;

internal class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(TasksDbContext dbContext) : base(dbContext) { }

    public async Task<User> CreateAsync(User user)
    {
        var userDb = user.ToDb();

        await _dbContext.AddAsync(userDb);
        await _dbContext.SaveChangesAsync();

        return userDb.ToDomain();
    }

    public async Task<bool> ExistsAsync(User userDto)
    {
        var user = await _dbContext.Users.Where(r => r.UserName == userDto.UserName && r.Password == userDto.Password).FirstOrDefaultAsync();
        return user != null;
    }
}
