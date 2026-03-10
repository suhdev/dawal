using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("greater_than_or_equal", "ge")]
  public class GreaterThanOrEqualFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(GreaterThanOrEqualFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }
      
      return Task.FromResult(Evaluate(values.First(), values.Last()));
    }

    private static object Evaluate(object firstVal, object secondVal)
    {
      if (firstVal is null)
      {
        return secondVal is null;
      }

      if (secondVal is null)
      {
        return true;
      }

      if (firstVal.GetType() != secondVal.GetType())
      {
        return false;
      }
      
      if (firstVal.IsDate())
      {
        return firstVal.CoerceToNumber() >= secondVal.CoerceToNumber();
      }

      if (firstVal.IsNumber())
      {
        return firstVal.ToNumber() >= secondVal.ToNumber();
      }

      if (firstVal is bool boolValue)
      {
        return boolValue || !(bool)secondVal;
      }

      if (firstVal is string stringValue)
      {
        return String.CompareOrdinal(stringValue, (string)secondVal) >= 0;
      }

      return false;
    }
  }
}