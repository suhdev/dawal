using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("add")]
  public class AddFunction : IEvaluationFunction
  {
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length == 0)
      {
        throw new InvalidNumberOfArgumentException(nameof(AddFunction),
          1,
          0);
      }

      if (values[0] is string str)
      {
        return Task.FromResult<object>(string.Concat(values));
      }

      if (values[0] is DateTime dateTime)
      {
        if (values.Length > 1)
        {
          if (values[1] is TimeSpan timeSpan)
          {
            return Task.FromResult<object>(dateTime + timeSpan);
          }

          return Task.FromResult<object>(dateTime + TimeSpan.FromDays((int)values[1].CoerceToNumber()));
        }

        return Task.FromResult<object>(dateTime);
      }
      
      if (values[0] is DateTimeOffset dateTimeOffset)
      {
        if (values.Length > 1)
        {
          if (values[1] is TimeSpan timeSpan)
          {
            return Task.FromResult<object>(dateTimeOffset + timeSpan);
          }

          return Task.FromResult<object>(dateTimeOffset + TimeSpan.FromDays((int)values[1].CoerceToNumber()));
        }

        return Task.FromResult<object>(dateTimeOffset);
      }

      return Task.FromResult<object>(values.Sum(v => ObjectExtensions.CoerceToNumber(v)));
    }
  }
}