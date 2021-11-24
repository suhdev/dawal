using System.Threading.Tasks;

namespace Dawal.Parser
{
  public interface IEvaluationFunction
  {
    Task<object> ExecuteAsync(IEvaluationContext context, params object[] values);
  }
}