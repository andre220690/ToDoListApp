namespace ToDoListApp.Dal.Models;
internal sealed class TaskDb
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long TaskStatusId { get;set; } 

    public TaskStatusDb TaskStatus { get; set; }
}
