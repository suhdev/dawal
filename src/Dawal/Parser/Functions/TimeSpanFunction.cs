using System;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("TimeSpan", "timespan", "time_span")]
  public class TimeSpanFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 1;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(TimeSpanFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      var days = values[0].CoerceToNumber();
      var hours = values.Length > 1
        ? values[1].CoerceToNumber()
        : 0;
      var minutes = values.Length > 2
        ? values[2].CoerceToNumber()
        : 0;
      var seconds = values.Length > 3
        ? values[3].CoerceToNumber()
        : 0;

      return new TimeSpan((int)days, (int)hours, (int)minutes, (int)seconds);
    }
  }
}