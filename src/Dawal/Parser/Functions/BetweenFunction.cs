using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("between")]
  public class BetweenFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 3;
    
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(BetweenFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      var value = values.First();
      if (value is null)
      {
        return Task.FromResult<object>(false);
      }
      
      if (value.IsNumber())
      {
        var val = value.CoerceToNumber();
        var min = values[1].CoerceToNumber();
        var max = values[2].CoerceToNumber();
        return Task.FromResult<object>(val >= min && val <= max);
      }

      if (value.IsDate())
      {
        var val = value.CoerceToDateTime();
        var min = values[1].CoerceToDateTime();
        var max = values[2].CoerceToDateTime();
        return Task.FromResult<object>(val >= min && val <= max);
      }

      return Task.FromResult<object>(false);
    }
  }
}