using SupportOps.Domain.Entities;

using AppEntity = SupportOps.Domain.Entities.Application;

namespace SupportOps.Application.Interfaces;

public interface IApplicationRepository
{
    Task<AppEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AppEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AppEntity application, CancellationToken cancellationToken = default);
}
