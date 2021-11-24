using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("not", "!")]
  public class NotFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException("not", ExpectedNumberOfArguments, values.Length);
      }

      var firstVal = values.First();

      if (firstVal is null)
      {
        return true;
      }

      return !firstVal.CoerceToBool();
    }
  }
}