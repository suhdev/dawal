using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("add")]
  public class AddFunction : IEvaluationFunction
  {
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(ConcatenateFunction),
          1,
          0);
      }

      if (values[0] is string str)
      {
        return string.Concat(values);
      }

      if (values[0] is DateTime dateTime)
      {
        if (values.Length > 1)
        {
          if (values[1] is TimeSpan timeSpan)
          {
            return dateTime + timeSpan;
          }

          return dateTime + TimeSpan.FromDays((int)values[1].CoerceToNumber());
        }

        return dateTime;
      }
      
      if (values[0] is DateTimeOffset dateTimeOffset)
      {
        if (values.Length > 1)
        {
          if (values[1] is TimeSpan timeSpan)
          {
            return dateTimeOffset + timeSpan;
          }

          return dateTimeOffset + TimeSpan.FromDays((int)values[1].CoerceToNumber());
        }

        return dateTimeOffset;
      }

      return values.Sum(v => ObjectExtensions.CoerceToNumber(v));
    }
  }
}