using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("find_one")]
  public class FindOneFunction : IEvaluationFunction
  {
    private const int MinimumNumberOfArguments = 3;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length < MinimumNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(nameof(FindOneFunction),
          MinimumNumberOfArguments,
          values.Length);
      }

      var firstValue = values[0];
      if (!(firstValue is IEnumerable list))
      {
        return firstValue;
      }

      var parameters = values.Skip(1).ToArray();
      var fns = new List<FindExecutionValue>();

      if (parameters.Length % 3 == 0)
      {
        for (var i = 0; i < parameters.Length; i += 3)
        {
          var prop = (string)parameters[i];
          var fn = parameters[i + 1].ToStringValue();
          var value = parameters[i + 2];

          fns.Add(new FindExecutionValue
          {
            FunctionName = fn, 
            PropName = prop,
            Value = value
          });
        }
      }
      else
      {
        for (var i = 0; i < parameters.Length; i += 2)
        {
          var prop = (string)parameters[i];
          var value = parameters[i + 1];

          fns.Add(new FindExecutionValue
          {
            FunctionName = "equal_to", 
            PropName = prop,
            Value = value
          });
        }
      }
      
      foreach (var item in list)
      {
        var result = await Task.WhenAll(fns.Select(async fn =>
        {
          var exec = context.GetFunction(fn.FunctionName);
          var propOfFn = context.GetFunction("prop_of");
          var propOfResult = await propOfFn.ExecuteAsync(context, item, fn.PropName);

          return await exec.ExecuteAsync(context, propOfResult, fn.Value);
        }));

        if (result.All(x => x != null && x.IsBool() && x.ToBool()))
        {
          return item;
        }
      }

      return null;
    }

    private class FindExecutionValue
    {
      public string FunctionName { get; set; }
      public string PropName { get; set; }
      public object Value { get; set; }
    }
  }
}