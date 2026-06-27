using System.Threading.Tasks;

namespace ConsoleDI.Services;

public interface IWorkerService
{
    Task ExecuteAsync(string inputPath);
}