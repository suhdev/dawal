using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("sum")]
  public class SumFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(SumFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return Task.FromResult<object>(values.Sum(x => x.CoerceToNumber()));
    }
  }
}