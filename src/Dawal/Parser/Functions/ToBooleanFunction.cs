using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("to_boolean", "bool", "toboolean", "boolean")]
  public class ToBooleanFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(nameof(ToNumberFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return values.First().CoerceToBool();
    }
  }
}