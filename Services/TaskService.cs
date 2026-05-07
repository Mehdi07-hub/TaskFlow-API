using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskResponse>> GetAllAsync(int userId)
    {
        var taches = await _db.Tasks
            .Include(t => t.Project)
            .Where(t => t.Project.UserId == userId)
            .ToListAsync();

        return taches.Select(t => ToResponse(t)).ToList();
    }

    public async Task<TaskResponse?> GetByIdAsync(int id, int userId)
    {
        var t = await _db.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (t == null || t.Project.UserId != userId) return null;
        return ToResponse(t);
    }

    public async Task<TaskResponse?> CreateAsync(CreateTaskRequest request, int userId)
    {
        var projet = await _db.Projects.FindAsync(request.ProjectId);
        if (projet == null || projet.UserId != userId) return null;

        var tache = new TaskItem
        {
            Title = request.Title,
            Status = request.Status,
            ProjectId = request.ProjectId,
            DueDate = request.DueDate
        };

        _db.Tasks.Add(tache);
        await _db.SaveChangesAsync();
        return ToResponse(tache);
    }

    public async Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request, int userId)
    {
        var t = await _db.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (t == null || t.Project.UserId != userId) return null;

        t.Title = request.Title;
        t.Status = request.Status;
        t.DueDate = request.DueDate;
        if (request.Comments != null) t.Comments = request.Comments;

        await _db.SaveChangesAsync();
        return ToResponse(t);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var t = await _db.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (t == null || t.Project.UserId != userId) return false;

        _db.Tasks.Remove(t);
        await _db.SaveChangesAsync();
        return true;
    }

    private TaskResponse ToResponse(TaskItem t) => new TaskResponse
    {
        Id = t.Id,
        Title = t.Title,
        Status = t.Status,
        ProjectId = t.ProjectId,
        DueDate = t.DueDate,
        Comments = t.Comments
    };
}