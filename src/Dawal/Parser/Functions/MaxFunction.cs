using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
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

      return values.Max(x =>
        x.IsDate()
        ? (object) x.CoerceToDateTime()
        : x.CoerceToNumber());
    }
  }
}