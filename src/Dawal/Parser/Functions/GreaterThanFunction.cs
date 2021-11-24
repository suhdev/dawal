using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("greater_than", "gt")]
  public class GreaterThanFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(GreaterThanFunction),
          ExpectedNumberOfArguments, 
          values.Length);
      }
      
      var firstVal = values.First();
      var secondVal = values.Last();

      if (firstVal is null)
      {
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

      if (firstVal.IsNumber())
      {
        return firstVal.ToNumber() > secondVal.ToNumber();
      }

      if (firstVal is bool boolValue)
      {
        return boolValue && true != (bool)secondVal;
      }

      if (firstVal is string stringValue)
      {
        return String.CompareOrdinal(stringValue, (string)secondVal) > 0;
      }

      return false;
    }
  }
}