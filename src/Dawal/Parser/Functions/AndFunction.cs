using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("and")]
  public class AndFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(AndFunction),
          1, values.Length);
      }
      
      return values.All(x => x.CoerceToBool());
    }
  }
}