using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Models;

namespace ToDoListApp.Dal.Extensions;

internal static class DbModelExtensions
{
    public static TaskDto ToDomain(this TaskDb taskDb) => new TaskDto
    {
        Id = taskDb.Id,
        Text = taskDb.Text,
        StatusId = taskDb.TaskStatusId
    };

    public static TaskDb ToDb(this TaskDto taskDto) => new TaskDb
    {
        Id = taskDto.Id,
        Text = taskDto.Text,
        TaskStatusId = taskDto.StatusId
    };

    public static User ToDomain(this UserDb userDb) => new User
    {
        UserName = userDb.UserName,
        Password = userDb.Password,
    };

    public static UserDb ToDb(this User user) => new UserDb
    {
        UserName = user.UserName,
        Password = user.Password,
    };
}
