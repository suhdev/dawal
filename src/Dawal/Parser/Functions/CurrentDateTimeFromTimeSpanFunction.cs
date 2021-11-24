using System;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("current_date_timespan", "currentdate_timespan", "now_timespan")]
  public class CurrentDateTimeFromTimeSpanFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 0;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(CurrentDateTimeFromTimeSpanFunction),
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
      

      return DateTimeOffset.UtcNow + 
             new TimeSpan((int)days, (int)hours, (int)minutes, (int)seconds);
    }
  }
}