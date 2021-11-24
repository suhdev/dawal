using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("sum")]
  public class SumFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(SumFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return values.Sum(x => ObjectExtensions.CoerceToNumber(x));
    }
  }
  
  [EvaluationFunction("max")]
  public class MaxFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(MaxFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return values.Max(x => x.CoerceToNumber());
    }
  }
}