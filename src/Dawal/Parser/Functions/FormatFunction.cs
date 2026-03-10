using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("format", "fmt")]
  public class FormatFunction : IEvaluationFunction
  {
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length < 1)
      {
        throw new InvalidNumberOfArgumentException(nameof(FormatFunction),
          1, 0);
      }

      if (values[0] is string strValue)
      {
        return Task.FromResult<object>(string.Format(strValue, values.Skip(1).ToArray()));
      }

      return Task.FromResult<object>(string.Format(values[0].ToString(), values.Skip(1).ToArray()));
    }
  }
}