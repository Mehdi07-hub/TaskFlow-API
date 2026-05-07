using System.ComponentModel.DataAnnotations;
using TaskFlow.Models;

namespace TaskFlow.DTOs;

public class RegisterRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required][EmailAddress] public string Email { get; set; } = string.Empty;
    [Required][MinLength(6)] public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required][EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}

public class CreateProjectRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public List<TaskResponse> Tasks { get; set; } = new();
}

public class CreateTaskRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public int ProjectId { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
}

public class UpdateTaskRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string>? Comments { get; set; }
}

public class TaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; }
    public int ProjectId { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Comments { get; set; } = new();
}