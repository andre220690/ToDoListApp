using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Dal.Repositories;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Dal;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TasksDbContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_SETTING");
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
