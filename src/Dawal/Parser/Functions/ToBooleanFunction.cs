using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("to_boolean", "bool", "toboolean", "boolean")]
  public class ToBooleanFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(nameof(ToBooleanFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return Task.FromResult<object>(values.First().CoerceToBool());
    }
  }
}