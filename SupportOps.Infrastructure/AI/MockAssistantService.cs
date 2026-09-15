using SupportOps.Application.Interfaces;

namespace SupportOps.Infrastructure.AI;

public class MockAssistantService : IAssistantService
{
    public Task<IncidentAnalysisResult> AnalyzeIncidentAsync(IncidentAnalysisRequest request, CancellationToken cancellationToken = default)
    {
        var lowerDesc = request.Description.ToLower();
        var lowerLogs = request.Logs.ToLower();

        string cause = "Não foi possível determinar a causa com exatidão.";
        string suggestions = "1. Analise os logs detalhadamente.\n2. Verifique o uso de recursos.\n3. Valide as integrações.";
        
        if (lowerDesc.Contains("timeout") || lowerLogs.Contains("timeout") || lowerDesc.Contains("lentidão"))
        {
            cause = "Esgotamento do pool de conexões com o banco ou lentidão na rede.";
            suggestions = "1. Verificar disponibilidade do banco de dados.\n2. Verificar quantidade máxima de conexões permitidas.\n3. Avaliar configuração do connection pool.";
        }
        else if (lowerDesc.Contains("500") || lowerLogs.Contains("exception") || lowerDesc.Contains("erro"))
        {
            cause = "Exceção não tratada no código ao processar a requisição.";
            suggestions = "1. Verificar a stack trace do erro.\n2. Validar se os dados de entrada estão corretos.\n3. Adicionar try-catch nos pontos críticos.";
        }
        
        var result = new IncidentAnalysisResult(
            PossibleCause: cause,
            Evidence: "Análise simulada baseada em palavras-chave presentes na descrição e nos logs.",
            Suggestions: suggestions,
            ConfidenceLevel: "Médio"
        );

        return Task.FromResult(result);
    }
}
