using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("min")]
  public class MinFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(MinFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return Task.FromResult(values.Min(x =>
        x.IsDate()
        ? (object) x.CoerceToDateTime()
        : x.CoerceToNumber()));
    }
  }
}