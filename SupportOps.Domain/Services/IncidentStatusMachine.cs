using SupportOps.Domain.Enums;
using SupportOps.Domain.Exceptions;

namespace SupportOps.Domain.Services;

public static class IncidentStatusMachine
{
    public static bool CanTransition(IncidentStatus current, IncidentStatus next)
    {
        return current switch
        {
            IncidentStatus.ABERTO => next is IncidentStatus.EM_ANALISE or IncidentStatus.CANCELADO,
            IncidentStatus.EM_ANALISE => next is IncidentStatus.AGUARDANDO_USUARIO or IncidentStatus.AGUARDANDO_TERCEIRO or IncidentStatus.EM_CORRECAO or IncidentStatus.CANCELADO,
            IncidentStatus.AGUARDANDO_USUARIO => next is IncidentStatus.EM_ANALISE or IncidentStatus.CANCELADO,
            IncidentStatus.AGUARDANDO_TERCEIRO => next is IncidentStatus.EM_ANALISE or IncidentStatus.CANCELADO,
            IncidentStatus.EM_CORRECAO => next is IncidentStatus.RESOLVIDO or IncidentStatus.EM_ANALISE,
            IncidentStatus.RESOLVIDO => next is IncidentStatus.ENCERRADO or IncidentStatus.EM_ANALISE,
            IncidentStatus.ENCERRADO => false, // Terminal
            IncidentStatus.CANCELADO => false, // Terminal
            _ => false
        };
    }
    
    public static void EnsureTransition(IncidentStatus current, IncidentStatus next)
    {
        if (!CanTransition(current, next))
        {
            throw new InvalidStatusTransitionException(current, next);
        }
    }
}
