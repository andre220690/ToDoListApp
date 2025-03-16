using FluentValidation;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Domain.Validation;

public class TaskDtoValidator : AbstractValidator<TaskDto>
{
    private readonly ITaskStatusRepository _taskStatusRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskDtoValidator(ITaskRepository taskRepository, ITaskStatusRepository taskStatusRepository)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _taskStatusRepository = taskStatusRepository ?? throw new ArgumentNullException(nameof(taskStatusRepository));

        RuleFor(r => r.Id)
            .MustAsync(async (Id, cancellationToken) =>
            {
                if (Id == 0)
                    return true;

                return await _taskRepository.ExistsAsync(Id);
            }).WithMessage("Указан не существующий Id");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Отсутствует текст задачи");

        RuleFor(r => r.StatusId)
            .MustAsync(async (StatusId, cancellationToken) =>
            {
                return await _taskStatusRepository.ExistsAsync(StatusId);
            }).WithMessage("Указан не существующий Id");
    }
}
