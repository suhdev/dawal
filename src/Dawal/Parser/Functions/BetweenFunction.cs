using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("between")]
  public class BetweenFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 3;
    
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(TimeSpanFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      var value = values.First();
      if (value is null)
      {
        return false;
      }
      
      if (value.IsNumber())
      {
        var val = value.CoerceToNumber();
        var min = values[1].CoerceToNumber();
        var max = values[2].CoerceToNumber();
        return val >= min && val <= max;
      }

      if (value.IsDate())
      {
        var val = value.CoerceToDateTime();
        var min = values[1].CoerceToDateTime();
        var max = values[2].CoerceToDateTime();
        return val >= min && val <= max;
      }

      return false;
    }
  }
}