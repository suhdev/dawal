using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("equal_to", "eq")]
  public class EqualToFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(EqualToFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }
      
      return Task.FromResult(Evaluate(values.First(), values.Last()));
    }

    private static object Evaluate(object firstVal, object secondVal)
    {
      if (firstVal is null && secondVal is null)
      {
        return true;
      }

      if (firstVal is null || secondVal is null)
      {
        return false;
      }

      if (firstVal.GetType() != secondVal.GetType())
      {
        return false;
      }

      if (firstVal.IsNumber())
      {
        return firstVal.ToNumber() == secondVal.ToNumber();
      }

      if (firstVal.IsDate())
      {
        return firstVal.CoerceToNumber().Equals(secondVal.CoerceToNumber());
      }
      
      if (firstVal is bool boolValue)
      {
        return boolValue == (bool)secondVal;
      }

      if (firstVal is string stringValue)
      {
        return stringValue == (string)secondVal;
      }

      return false;
    }
  }
}