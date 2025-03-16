using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Core.Models;
using ToDoListApp.Domain.Services;
using ToDoListApp.Domain.Services.Interfaces;
using ToDoListApp.Domain.Validation;

namespace ToDoListApp.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ITaskDataService, TaskDataService>();
        services.AddScoped<IHubSenderService, HubSenderService>();

        services.AddScoped<IValidator<TaskDto>, TaskDtoValidator>();

        return services;
    }
}
