using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("average", "avg")]
  public class AverageFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(AverageFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return Task.FromResult<object>(values.Average(x => x.CoerceToNumber()));
    }
  }
}