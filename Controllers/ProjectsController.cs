using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Services;

namespace TaskFlow.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET api/projects
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projets = await _projectService.GetAllAsync(GetUserId());
        return Ok(projets);
    }

    // GET api/projects/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var projet = await _projectService.GetByIdAsync(id, GetUserId());
        if (projet == null) return NotFound(new { message = "Projet introuvable." });
        return Ok(projet);
    }

    // POST api/projects
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var projet = await _projectService.CreateAsync(request, GetUserId());
        return StatusCode(201, projet);
    }

    // PUT api/projects/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateProjectRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var projet = await _projectService.UpdateAsync(id, request, GetUserId());
        if (projet == null) return NotFound(new { message = "Projet introuvable." });
        return Ok(projet);
    }

    // DELETE api/projects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.DeleteAsync(id, GetUserId());
        if (!result) return NotFound(new { message = "Projet introuvable." });
        return NoContent();
    }
}