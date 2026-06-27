using CommandLine;
using ConsoleDI.Options;
using ConsoleDI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

// 1. Analyse des arguments de la ligne de commande dès le point d'entrée
var parseResult = Parser.Default.ParseArguments<AppOptions>(args);

// Si les arguments sont invalides (ou --help), CommandLineParser gère l'affichage et on quitte avec le code 1
if (parseResult.Tag == ParserResultType.NotParsed)
{
    return 1;
}

var opts = ((Parsed<AppOptions>)parseResult).Value;

// 2. Configuration globale de Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(opts.Verbose ? LogEventLevel.Debug : LogEventLevel.Information)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Démarrage du Host de l'application...");

    // 3. Initialisation légère du Host .NET
    using IHost host = Host.CreateDefaultBuilder()
        .UseSerilog()
        .ConfigureServices((context, services) =>
        {
            // Enregistrement de vos services et injection de vos options typées si besoin
            services.AddSingleton(opts);
            services.AddTransient<IWorkerService, WorkerService>();
        })
        .Build();

    // 4. Résolution et exécution de la logique métier
    var worker = host.Services.GetRequiredService<IWorkerService>();
    await worker.ExecuteAsync(opts.InputPath);

    return 0; // Code de sortie de l'application (Succès)
}
catch (Exception ex)
{
    Log.Fatal(ex, "L'application s'est arrêtée de manière inattendue.");
    return 1; // Code de sortie (Échec)
}
finally
{
    await Log.CloseAndFlushAsync();
}