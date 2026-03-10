using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("not", "!")]
  public class NotFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(nameof(NotFunction), ExpectedNumberOfArguments, values.Length);
      }

      var firstVal = values.First();

      if (firstVal is null)
      {
        return Task.FromResult<object>(true);
      }

      return Task.FromResult<object>(!firstVal.CoerceToBool());
    }
  }
}