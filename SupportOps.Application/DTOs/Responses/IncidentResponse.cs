using SupportOps.Domain.Enums;

namespace SupportOps.Application.DTOs.Responses;

public class IncidentResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ApplicationId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public Guid RequesterId { get; set; }
    public string RequesterName { get; set; } = string.Empty;
    public Guid? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
    public IncidentCategory Category { get; set; }
    public IncidentPriority Priority { get; set; }
    public IncidentStatus Status { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime SlaDeadline { get; set; }
    public SlaStatus SlaStatus { get; set; }
}
