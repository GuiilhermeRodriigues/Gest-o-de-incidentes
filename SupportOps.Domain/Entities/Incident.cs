using SupportOps.Domain.Enums;
using SupportOps.Domain.Services;

namespace SupportOps.Domain.Entities;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public Guid ApplicationId { get; set; }
    public Application? Application { get; set; }
    
    public Guid RequesterId { get; set; }
    public User? Requester { get; set; }
    
    public Guid? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    
    public IncidentCategory Category { get; set; }
    public IncidentPriority Impact { get; set; }
    public IncidentPriority Urgency { get; set; }
    
    public IncidentPriority Priority { get; set; }
    public IncidentStatus Status { get; set; } = IncidentStatus.ABERTO;
    
    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    
    public DateTime SlaDeadline { get; set; }
    
    public string? Diagnosis { get; set; }
    public string? Solution { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<IncidentHistory> History { get; set; } = new List<IncidentHistory>();

    public void ChangeStatus(IncidentStatus newStatus)
    {
        IncidentStatusMachine.EnsureTransition(this.Status, newStatus);
        this.Status = newStatus;
        this.UpdatedAt = DateTime.UtcNow;
    }
}
