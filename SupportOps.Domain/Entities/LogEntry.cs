namespace SupportOps.Domain.Entities;

public class LogEntry
{
    public string? Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Application { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? TraceId { get; set; }
    public string? IncidentId { get; set; }
    public string? Metadata { get; set; }
}
