using SupportOps.Domain.Enums;

namespace SupportOps.Domain.Entities;

public class Application
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public ApplicationEnvironment Environment { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.ATIVO;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
