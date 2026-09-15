using SupportOps.Domain.Enums;

namespace SupportOps.Application.DTOs.Requests;

public class CreateIncidentRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ApplicationId { get; set; }
    public Guid RequesterId { get; set; }
    public IncidentCategory Category { get; set; }
    public IncidentPriority Impact { get; set; }
    public IncidentPriority Urgency { get; set; }
}
