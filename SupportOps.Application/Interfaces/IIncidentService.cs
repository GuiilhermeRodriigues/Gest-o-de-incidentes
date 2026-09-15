using SupportOps.Application.DTOs.Requests;
using SupportOps.Application.DTOs.Responses;
using SupportOps.Domain.Enums;

namespace SupportOps.Application.Interfaces;

public interface IIncidentService
{
    Task<IncidentResponse> CreateAsync(CreateIncidentRequest request, CancellationToken cancellationToken = default);
    Task<IncidentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<IncidentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AssignAsync(Guid incidentId, Guid assigneeId, Guid userId, CancellationToken cancellationToken = default);
    Task ChangeStatusAsync(Guid incidentId, IncidentStatus newStatus, Guid userId, CancellationToken cancellationToken = default);
}
