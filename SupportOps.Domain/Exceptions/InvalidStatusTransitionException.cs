using SupportOps.Domain.Enums;

namespace SupportOps.Domain.Exceptions;

public class InvalidStatusTransitionException : DomainException
{
    public InvalidStatusTransitionException(IncidentStatus current, IncidentStatus next) 
        : base($"Não é possível alterar o status do incidente de {current} para {next}.")
    {
    }
}
