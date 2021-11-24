using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("prop_of")]
  public class PropOfFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != 2)
      {
        throw new Exception($"Expected two operands for prop of operator but got {values.Length}");
      }
      
      var firstVal = values.First();
      var secondVal = values.Last();

      if (firstVal is null || secondVal is null)
      {
        return null;
      }

      if (!(secondVal is string key))
      {
        throw new Exception($"Expected second parameter of prop of to be string but got {secondVal} instead");
      }

      return firstVal.GetProperty(key);
    }
  }
}