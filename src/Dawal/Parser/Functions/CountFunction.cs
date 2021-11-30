using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("count")]
  public class CountFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != 1)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(CountFunction),
          1, values.Length);
      }

      if (values[0] is string str)
      {
        return str.Length;
      }

      if (values[0].IsNumber())
      {
        return values[0];
      }

      return ((IEnumerable)values[0]).OfType<object>().Count();
    }
  }
}