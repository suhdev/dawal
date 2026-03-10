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
      var fns = FindConditionParser.Parse(parameters);
      
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
  }
}