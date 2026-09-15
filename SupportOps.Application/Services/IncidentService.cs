using SupportOps.Application.DTOs.Requests;
using SupportOps.Application.DTOs.Responses;
using SupportOps.Application.Interfaces;
using SupportOps.Domain.Entities;
using SupportOps.Domain.Enums;
using SupportOps.Domain.Exceptions;
using SupportOps.Domain.Services;
using AppEntity = SupportOps.Domain.Entities.Application;

namespace SupportOps.Application.Services;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserRepository _userRepository;

    public IncidentService(
        IIncidentRepository incidentRepository,
        IApplicationRepository applicationRepository,
        IUserRepository userRepository)
    {
        _incidentRepository = incidentRepository;
        _applicationRepository = applicationRepository;
        _userRepository = userRepository;
    }

    public async Task<IncidentResponse> CreateAsync(CreateIncidentRequest request, CancellationToken cancellationToken = default)
    {
        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId, cancellationToken);
        if (application == null)
            throw new DomainException("Aplicação não encontrada.");

        var requester = await _userRepository.GetByIdAsync(request.RequesterId, cancellationToken);
        if (requester == null)
            throw new DomainException("Solicitante não encontrado.");

        var priority = PriorityCalculator.Calculate(request.Impact, request.Urgency);
        var openedAt = DateTime.UtcNow;
        var deadline = SlaCalculator.CalculateDeadline(openedAt, priority);

        var incident = new Incident
        {
            Number = $"INC-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}",
            Title = request.Title,
            Description = request.Description,
            ApplicationId = request.ApplicationId,
            RequesterId = request.RequesterId,
            Category = request.Category,
            Impact = request.Impact,
            Urgency = request.Urgency,
            Priority = priority,
            OpenedAt = openedAt,
            SlaDeadline = deadline
        };
        
        incident.History.Add(new IncidentHistory
        {
            Action = "Criado",
            NewValue = IncidentStatus.ABERTO.ToString(),
            UserId = request.RequesterId,
            CreatedAt = DateTime.UtcNow
        });

        await _incidentRepository.AddAsync(incident, cancellationToken);
        
        return MapToResponse(incident);
    }

    public async Task<IncidentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var incident = await _incidentRepository.GetByIdAsync(id, cancellationToken);
        if (incident == null)
            throw new DomainException("Incidente não encontrado.");
            
        return MapToResponse(incident);
    }

    public async Task<IEnumerable<IncidentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var incidents = await _incidentRepository.GetAllAsync(cancellationToken);
        return incidents.Select(MapToResponse);
    }

    public async Task AssignAsync(Guid incidentId, Guid assigneeId, Guid userId, CancellationToken cancellationToken = default)
    {
        var incident = await _incidentRepository.GetByIdAsync(incidentId, cancellationToken);
        if (incident == null)
            throw new DomainException("Incidente não encontrado.");

        var assignee = await _userRepository.GetByIdAsync(assigneeId, cancellationToken);
        if (assignee == null)
            throw new DomainException("Analista não encontrado.");

        var oldAssignee = incident.AssigneeId?.ToString() ?? "Nenhum";
        incident.AssigneeId = assigneeId;
        
        incident.History.Add(new IncidentHistory
        {
            Action = "Atribuído",
            OldValue = oldAssignee,
            NewValue = assigneeId.ToString(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        });

        await _incidentRepository.UpdateAsync(incident, cancellationToken);
    }

    public async Task ChangeStatusAsync(Guid incidentId, IncidentStatus newStatus, Guid userId, CancellationToken cancellationToken = default)
    {
        var incident = await _incidentRepository.GetByIdAsync(incidentId, cancellationToken);
        if (incident == null)
            throw new DomainException("Incidente não encontrado.");

        var oldStatus = incident.Status;
        incident.ChangeStatus(newStatus);
        
        if (newStatus == IncidentStatus.RESOLVIDO)
            incident.ResolvedAt = DateTime.UtcNow;
            
        if (newStatus == IncidentStatus.ENCERRADO || newStatus == IncidentStatus.CANCELADO)
            incident.ClosedAt = DateTime.UtcNow;
            
        incident.History.Add(new IncidentHistory
        {
            Action = "Status alterado",
            OldValue = oldStatus.ToString(),
            NewValue = newStatus.ToString(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        });

        await _incidentRepository.UpdateAsync(incident, cancellationToken);
    }

    private IncidentResponse MapToResponse(Incident incident)
    {
        return new IncidentResponse
        {
            Id = incident.Id,
            Number = incident.Number,
            Title = incident.Title,
            Description = incident.Description,
            ApplicationId = incident.ApplicationId,
            ApplicationName = incident.Application?.Name ?? string.Empty,
            RequesterId = incident.RequesterId,
            RequesterName = incident.Requester?.Name ?? string.Empty,
            AssigneeId = incident.AssigneeId,
            AssigneeName = incident.Assignee?.Name,
            Category = incident.Category,
            Priority = incident.Priority,
            Status = incident.Status,
            OpenedAt = incident.OpenedAt,
            SlaDeadline = incident.SlaDeadline,
            SlaStatus = SlaCalculator.GetStatus(incident.SlaDeadline, DateTime.UtcNow, incident.Priority)
        };
    }
}
