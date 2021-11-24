using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("or")]
  public class OrFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(OrFunction),
          1, values.Length);
      }
      
      return values.Any(x =>  x.CoerceToBool());
    }
  }
}