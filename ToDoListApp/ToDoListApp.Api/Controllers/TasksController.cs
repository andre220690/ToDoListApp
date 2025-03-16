using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Core.Models;
using ToDoListApp.Domain.Services.Interfaces;

namespace ToDoListApp.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskDataService _taskDataService;

    public TasksController(ITaskDataService taskDataService)
    {
        _taskDataService = taskDataService ?? throw new ArgumentNullException(nameof(taskDataService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTaskAsync()
    {
        var tasks = await _taskDataService.GetAllTaskAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync(TaskDto taskDto)
    {
        var tasks = await _taskDataService.CreateTaskAsync(taskDto);
        return Ok(tasks);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TaskDto taskDto)
    {
        var task = await _taskDataService.UpdateTaskAsync(taskDto);
        return Ok(task);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTaskAsync(long id)
    {
        await _taskDataService.DeleteTaskAsync(id);
        return Ok();
    }
}