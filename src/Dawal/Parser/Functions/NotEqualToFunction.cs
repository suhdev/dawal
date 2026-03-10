using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("not_equal", "ne")]
  public class NotEqualToFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(NotEqualToFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }
      
      return Task.FromResult(Evaluate(values.First(), values.Last()));
    }

    private static object Evaluate(object firstVal, object secondVal)
    {
      if (firstVal is null && secondVal is null)
      {
        return false;
      }

      if (firstVal is null || secondVal is null)
      {
        return true;
      }

      if (firstVal.GetType() != secondVal.GetType())
      {
        return true;
      }

      if (firstVal.IsNumber())
      {
        return firstVal.ToNumber() != secondVal.ToNumber();
      }
      
      if (firstVal.IsDate())
      {
        return !firstVal.CoerceToNumber().Equals(secondVal.CoerceToNumber());
      }

      if (firstVal.IsBool())
      {
        return firstVal.ToBool() != secondVal.ToBool();
      }

      if (firstVal.IsString())
      {
        return !firstVal.ToStringValue().Equals(secondVal.ToStringValue());
      }

      return true;
    }
  }
}