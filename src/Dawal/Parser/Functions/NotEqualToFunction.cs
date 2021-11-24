using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("not_equal", "ne")]
  public class NotEqualToFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 2;
    public async Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(NotEqualToFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }
      
      var firstVal = values.First();
      var secondVal = values.Last();

      if (firstVal is null && secondVal is null)
      {
        return false;
      }

      if (firstVal is null || secondVal is null)
      {
        return true;
      }

      if (firstVal.GetType() != secondVal.GetType())
      {
        return true;
      }

      if (firstVal.IsNumber())
      {
        return firstVal.ToNumber() != secondVal.ToNumber();
      }

      if (firstVal.IsBool())
      {
        return firstVal.ToBool() != secondVal.ToBool();
      }

      if (firstVal.IsString())
      {
        return !firstVal.ToStringValue().Equals(secondVal.ToStringValue());
      }

      return true;
    }
  }
}