using CommandLine;

namespace ConsoleDI.Options;

public class AppOptions
{
    [Option('i', "input", Required = true, HelpText = "Le chemin vers le fichier d'entrée.")]
    public string InputPath { get; set; } = string.Empty;

    [Option('v', "verbose", Required = false, Default = false, HelpText = "Active le mode de log détaillé (Debug).")]
    public bool Verbose { get; set; }
}