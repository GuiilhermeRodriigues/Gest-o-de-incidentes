using Microsoft.AspNetCore.Mvc;
using SupportOps.Application.DTOs.Requests;
using SupportOps.Application.DTOs.Responses;
using SupportOps.Application.Interfaces;
using SupportOps.Domain.Enums;

namespace SupportOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _incidentService;
    private readonly IAssistantService _assistantService;

    public IncidentsController(IIncidentService incidentService, IAssistantService assistantService)
    {
        _incidentService = incidentService;
        _assistantService = assistantService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncidentResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var incidents = await _incidentService.GetAllAsync(cancellationToken);
        return Ok(incidents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var incident = await _incidentService.GetByIdAsync(id, cancellationToken);
        return Ok(incident);
    }

    [HttpPost]
    public async Task<ActionResult<IncidentResponse>> Create([FromBody] CreateIncidentRequest request, CancellationToken cancellationToken)
    {
        var incident = await _incidentService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = incident.Id }, incident);
    }

    [HttpPost("{id}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] Guid assigneeId, CancellationToken cancellationToken)
    {
        var userId = Guid.Empty;
        await _incidentService.AssignAsync(id, assigneeId, userId, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/change-status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] IncidentStatus newStatus, CancellationToken cancellationToken)
    {
        var incident = await _incidentService.GetByIdAsync(id, cancellationToken);
        var userId = incident.RequesterId;
        await _incidentService.ChangeStatusAsync(id, newStatus, userId, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/analyze")]
    public async Task<ActionResult<IncidentAnalysisResult>> Analyze(Guid id, CancellationToken cancellationToken)
    {
        var incident = await _incidentService.GetByIdAsync(id, cancellationToken);
        
        var request = new IncidentAnalysisRequest(
            incident.Title, 
            incident.Description, 
            incident.ApplicationName, 
            incident.Priority.ToString(), 
            "Simulated Logs: DB Timeout occurred...", 
            "No history yet"
        );
        
        var result = await _assistantService.AnalyzeIncidentAsync(request, cancellationToken);
        return Ok(result);
    }
}
