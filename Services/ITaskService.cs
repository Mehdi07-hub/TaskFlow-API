using TaskFlow.DTOs;

namespace TaskFlow.Services;

public interface ITaskService
{
    Task<List<TaskResponse>> GetAllAsync(int userId);
    Task<TaskResponse?> GetByIdAsync(int id, int userId);
    Task<TaskResponse?> CreateAsync(CreateTaskRequest request, int userId);
    Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}