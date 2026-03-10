using System.Threading;

namespace Dawal.Parser
{
  public interface IEvaluationContext
  {
    CancellationToken CancellationToken { get; set; }
    IEvaluationFunction GetFunction(string identifier);
    object GetVariable(string name);
  }
}