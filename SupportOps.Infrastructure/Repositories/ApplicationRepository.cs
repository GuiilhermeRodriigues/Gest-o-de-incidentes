using Microsoft.EntityFrameworkCore;
using SupportOps.Application.Interfaces;
using SupportOps.Domain.Entities;
using SupportOps.Infrastructure.Data;
using AppEntity = SupportOps.Domain.Entities.Application;

namespace SupportOps.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;

    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Applications.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<AppEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Applications.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AppEntity application, CancellationToken cancellationToken = default)
    {
        await _context.Applications.AddAsync(application, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
