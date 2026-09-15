namespace SupportOps.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid IncidentId { get; set; }
    public Incident? Incident { get; set; }
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
