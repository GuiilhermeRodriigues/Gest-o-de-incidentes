namespace SupportOps.Domain.Entities;

public class KnowledgeArticle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Problem { get; set; } = string.Empty;
    public string Cause { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    
    public Guid? IncidentId { get; set; }
    public Incident? Incident { get; set; }
    
    public Guid AuthorId { get; set; }
    public User? Author { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? Tags { get; set; }
}
