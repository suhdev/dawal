using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("greater_than_or_equal", "ge")]
  public class GreaterThanOrEqualFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(GreaterThanOrEqualFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }
      
      var firstVal = values.First();
      var secondVal = values.Last();

      if (firstVal is null)
      {
        if (secondVal is null)
        {
          return true;
        }

        return false;
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
        return boolValue || firstVal.ToBool() == secondVal.ToBool();
      }

      if (firstVal is string stringValue)
      {
        return String.CompareOrdinal(stringValue, (string)secondVal) >= 0;
      }

      return false;
    }
  }
}