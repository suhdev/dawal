using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("concat", "concatenate")]
  public class ConcatenateFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(ConcatenateFunction),
          1,
          0);
      }
      
      return string.Concat(values);
    }
  }
}