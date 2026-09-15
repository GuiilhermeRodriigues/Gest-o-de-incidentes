using MongoDB.Driver;
using SupportOps.Application.Interfaces;
using SupportOps.Domain.Entities;

namespace SupportOps.Infrastructure.MongoDB;

public class LogRepository : ILogRepository
{
    private readonly MongoDbContext _context;

    public LogRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task AddLogAsync(LogEntry log, CancellationToken cancellationToken = default)
    {
        await _context.Logs.InsertOneAsync(log, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<LogEntry>> GetLogsByIncidentAsync(string incidentId, CancellationToken cancellationToken = default)
    {
        return await _context.Logs.Find(l => l.IncidentId == incidentId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<LogEntry>> GetRecentLogsAsync(int count = 100, CancellationToken cancellationToken = default)
    {
        return await _context.Logs.Find(_ => true)
                                  .SortByDescending(l => l.Timestamp)
                                  .Limit(count)
                                  .ToListAsync(cancellationToken);
    }
}
