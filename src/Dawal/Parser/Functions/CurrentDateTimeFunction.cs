using System;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("current_date", "currentdate", "now")]
  public class CurrentDateTimeFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 0;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != 0)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(CurrentDateTimeFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      return DateTimeOffset.UtcNow;
    }
  }
}