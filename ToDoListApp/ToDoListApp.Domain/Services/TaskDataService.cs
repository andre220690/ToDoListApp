using FluentValidation;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Repositories.Interfaces;
using ToDoListApp.Domain.Services.Interfaces;

namespace ToDoListApp.Domain.Services;

public class TaskDataService : ITaskDataService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<TaskDto> _taskValidator;
    private readonly IHubSenderService _hubSenderService;

    private long STATUS_ID_NEW_TASK = 1;

    public TaskDataService(ITaskRepository taskRepository, IValidator<TaskDto> taskValidator, IHubSenderService hubSenderService)
    {
        _taskRepository  = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _taskValidator = taskValidator ?? throw new ArgumentNullException(nameof(taskValidator));
        _hubSenderService = hubSenderService ?? throw new ArgumentNullException(nameof(hubSenderService));;
    }

    public Task<IEnumerable<TaskDto>> GetAllTaskAsync()
    {
        var tasks = _taskRepository.GetAllAsync();
        return tasks;
    }

    public async Task<TaskDto> UpdateTaskAsync(TaskDto taskDto)
    {
        var validationResult = await _taskValidator.ValidateAsync(taskDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }

        var updatedTask = await _taskRepository.UpdateAsync(taskDto);
        await _hubSenderService.InvokeUpdateTaskAsync(updatedTask);

        return updatedTask;
    }

    public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
    {
        var validationResult = await _taskValidator.ValidateAsync(taskDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }

        var createdTask = await _taskRepository.CreateAsync(taskDto);
        await _hubSenderService.InvokeAddTaskAsync(createdTask);

        return createdTask;
    }

    public async Task DeleteTaskAsync(long taskId)
    {
        var taskDto = await _taskRepository.GetByIdAsync(taskId);
        if (taskDto == null)
        {
            throw new ValidationException("Отсутствует удаляемый объект"); // TODO: посмотреть что возвращает
        }

        await _taskRepository.DeleteAsync(taskId);
        await _hubSenderService.InvokeDeleteTaskAsync(taskId);
    }
}
