namespace ToDoListApp.Core.Models;
public class TaskDto
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long StatusId { get; set; }
}