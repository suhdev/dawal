using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("context", "ctx")]
  public class ContextFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 0;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(ContextFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return Task.FromResult<object>(context);
    }
  }
}