namespace SupportOps.Application.Interfaces;

public record IncidentAnalysisRequest(string Title, string Description, string ApplicationName, string Priority, string Logs, string History);

public record IncidentAnalysisResult(string PossibleCause, string Evidence, string Suggestions, string ConfidenceLevel);

public interface IAssistantService
{
    Task<IncidentAnalysisResult> AnalyzeIncidentAsync(IncidentAnalysisRequest request, CancellationToken cancellationToken = default);
}
