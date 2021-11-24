using System.Threading.Tasks;

namespace Dawal.Parser
{
  public interface IEvaluationNode
  {
    Task<object> EvaluateAsync(IEvaluationContext context);
    Task<TResult> EvaluateAsync<TResult>(IEvaluationContext context);
  }
}