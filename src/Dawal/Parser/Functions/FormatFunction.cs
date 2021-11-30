using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("format", "fmt")]
  public class FormatFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length < 1)
      {
        throw new InvalidNumberOfArgumentException(nameof(FormatFunction),
          1, 0);
      }

      if (values[0] is string strValue)
      {
        return string.Format(strValue, values.Skip(1).ToArray());
      }

      return string.Format(values[0].ToString(), values.Skip(1).ToArray());
    }
  }
}