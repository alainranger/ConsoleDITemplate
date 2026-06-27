using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleDI.Services;

public class WorkerService(ILogger<WorkerService> logger) : IWorkerService
{
    private readonly ILogger<WorkerService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task ExecuteAsync(string inputPath)
    {
        _logger.LogInformation("Début du traitement...");
        _logger.LogDebug("Fichier en cours d'analyse : {Path}", inputPath);

        // Simulation du travail
        await Task.Delay(500);

        _logger.LogInformation("Traitement terminé avec succès.");
    }
}