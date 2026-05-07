namespace TaskFlow.Models;

// on appelle ca TaskItem car "Task" est un mot reserve en C#
public enum TaskItemStatus { Todo, InProgress, Done }

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
    public DateTime? DueDate { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public List<string> Comments { get; set; } = new List<string>();
}