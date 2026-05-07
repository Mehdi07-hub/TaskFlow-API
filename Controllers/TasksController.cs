using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET api/tasks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var taches = await _taskService.GetAllAsync(GetUserId());
        return Ok(taches);
    }

    // GET api/tasks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tache = await _taskService.GetByIdAsync(id, GetUserId());
        if (tache == null) return NotFound(new { message = "Tache introuvable." });
        return Ok(tache);
    }

    // POST api/tasks
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var tache = await _taskService.CreateAsync(request, GetUserId());
        if (tache == null) return NotFound(new { message = "Projet introuvable." });
        return StatusCode(201, tache);
    }

    // PUT api/tasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var tache = await _taskService.UpdateAsync(id, request, GetUserId());
        if (tache == null) return NotFound(new { message = "Tache introuvable." });
        return Ok(tache);
    }

    // DELETE api/tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskService.DeleteAsync(id, GetUserId());
        if (!result) return NotFound(new { message = "Tache introuvable." });
        return NoContent();
    }
}