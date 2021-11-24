using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("less_than_or_equal", "le")]
  public class LessThanOrEqualFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(LessThanOrEqualFunction),
            ExpectedNumberOfArguments, values.Length);
      }
      
      var firstVal = values.First();
      var secondVal = values.Last();

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
        return firstVal.ToNumber() <= secondVal.ToNumber();
      }

      if (firstVal is bool boolValue)
      {
        return boolValue == (bool)secondVal || 
               boolValue == false && (bool)secondVal;
      }

      if (firstVal is string stringValue)
      {
        return String.CompareOrdinal(stringValue, (string)secondVal) <= 0;
      }

      return false;
    }
  }
}