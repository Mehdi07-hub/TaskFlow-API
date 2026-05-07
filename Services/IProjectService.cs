using TaskFlow.DTOs;

namespace TaskFlow.Services;

public interface IProjectService
{
    Task<List<ProjectResponse>> GetAllAsync(int userId);
    Task<ProjectResponse?> GetByIdAsync(int id, int userId);
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, int userId);
    Task<ProjectResponse?> UpdateAsync(int id, CreateProjectRequest request, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}