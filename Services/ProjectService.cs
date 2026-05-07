using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _db;

    public ProjectService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProjectResponse>> GetAllAsync(int userId)
    {
        var projets = await _db.Projects
            .Include(p => p.Tasks)
            .Where(p => p.UserId == userId)
            .ToListAsync();

        return projets.Select(p => ToResponse(p)).ToList();
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id, int userId)
    {
        var p = await _db.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null || p.UserId != userId) return null;
        return ToResponse(p);
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, int userId)
    {
        var p = new Project
        {
            Name = request.Name,
            Description = request.Description,
            UserId = userId,
            CreationDate = DateTime.UtcNow
        };

        _db.Projects.Add(p);
        await _db.SaveChangesAsync();
        return ToResponse(p);
    }

    public async Task<ProjectResponse?> UpdateAsync(int id, CreateProjectRequest request, int userId)
    {
        var p = await _db.Projects.FindAsync(id);
        if (p == null || p.UserId != userId) return null;

        p.Name = request.Name;
        p.Description = request.Description;
        await _db.SaveChangesAsync();
        return ToResponse(p);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var p = await _db.Projects.FindAsync(id);
        if (p == null || p.UserId != userId) return false;

        _db.Projects.Remove(p);
        await _db.SaveChangesAsync();
        return true;
    }

    private ProjectResponse ToResponse(Project p) => new ProjectResponse
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        CreationDate = p.CreationDate,
        Tasks = p.Tasks.Select(t => new TaskResponse
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
            ProjectId = t.ProjectId,
            DueDate = t.DueDate,
            Comments = t.Comments
        }).ToList()
    };
}