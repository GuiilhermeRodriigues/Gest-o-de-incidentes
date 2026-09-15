using SupportOps.Domain.Entities;

namespace SupportOps.Application.Interfaces;

public interface ILogRepository
{
    Task AddLogAsync(LogEntry log, CancellationToken cancellationToken = default);
    Task<IEnumerable<LogEntry>> GetLogsByIncidentAsync(string incidentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LogEntry>> GetRecentLogsAsync(int count = 100, CancellationToken cancellationToken = default);
}
