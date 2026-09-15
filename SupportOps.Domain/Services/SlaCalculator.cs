using SupportOps.Domain.Enums;

namespace SupportOps.Domain.Services;

public static class SlaCalculator
{
    public static int GetSlaHours(IncidentPriority priority)
    {
        return priority switch
        {
            IncidentPriority.CRITICA => 2,
            IncidentPriority.ALTA => 8,
            IncidentPriority.MEDIA => 24,
            IncidentPriority.BAIXA => 48,
            _ => 24
        };
    }

    public static DateTime CalculateDeadline(DateTime openedAt, IncidentPriority priority)
    {
        return openedAt.AddHours(GetSlaHours(priority));
    }
    
    public static SlaStatus GetStatus(DateTime deadline, DateTime now, IncidentPriority priority)
    {
        if (now > deadline)
            return SlaStatus.VENCIDO;
            
        var totalSlaHours = GetSlaHours(priority);
        var remainingHours = (deadline - now).TotalHours;
        
        if (remainingHours <= totalSlaHours * 0.2)
            return SlaStatus.EM_RISCO;
            
        return SlaStatus.DENTRO_DO_PRAZO;
    }
}
