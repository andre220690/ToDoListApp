namespace ToDoListApp.Dal.Models;
internal sealed class TaskStatusDb
{
    public long Id { get; set; }
    public string TaskStatusName { get; set; }

    public IEnumerable<TaskDb> Tasks { get; set; }
}
