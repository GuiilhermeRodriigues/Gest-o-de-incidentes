using SupportOps.Domain.Enums;

namespace SupportOps.Domain.Services;

public static class PriorityCalculator
{
    public static IncidentPriority Calculate(IncidentPriority impact, IncidentPriority urgency)
    {
        if (impact == IncidentPriority.CRITICA || urgency == IncidentPriority.CRITICA)
            return IncidentPriority.CRITICA;

        if (impact == IncidentPriority.ALTA && urgency == IncidentPriority.ALTA)
            return IncidentPriority.CRITICA;

        if ((impact == IncidentPriority.ALTA && urgency == IncidentPriority.MEDIA) ||
            (impact == IncidentPriority.MEDIA && urgency == IncidentPriority.ALTA))
            return IncidentPriority.ALTA;

        if (impact == IncidentPriority.MEDIA && urgency == IncidentPriority.MEDIA)
            return IncidentPriority.MEDIA;

        if (impact == IncidentPriority.BAIXA && urgency == IncidentPriority.BAIXA)
            return IncidentPriority.BAIXA;

        // Fallbacks
        return IncidentPriority.MEDIA;
    }
}
