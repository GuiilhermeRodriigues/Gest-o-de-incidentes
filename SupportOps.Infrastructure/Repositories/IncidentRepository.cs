using Microsoft.EntityFrameworkCore;
using SupportOps.Application.Interfaces;
using SupportOps.Domain.Entities;
using SupportOps.Infrastructure.Data;

namespace SupportOps.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Incidents
            .Include(i => i.Application)
            .Include(i => i.Requester)
            .Include(i => i.Assignee)
            .Include(i => i.Comments)
            .Include(i => i.History)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Incident>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Incidents
            .Include(i => i.Application)
            .Include(i => i.Assignee)
            .OrderByDescending(i => i.OpenedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Incident incident, CancellationToken cancellationToken = default)
    {
        await _context.Incidents.AddAsync(incident, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default)
    {
        // Entity is already tracked by the context since it was fetched without AsNoTracking.
        // Calling Update() causes issues with navigation properties being marked as modified.
        await _context.SaveChangesAsync(cancellationToken);
    }
}
