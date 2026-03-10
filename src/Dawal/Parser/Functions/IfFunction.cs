using System.Threading.Tasks;

namespace Dawal.Parser.Functions
{
  [EvaluationFunction("if")]
  public class IfFunction : IEvaluationFunction
  {
    private const int ExpectedNumberOfArguments = 3;
    public Task<object> ExecuteAsync(IEvaluationContext context, params object[] values)
    {
      if (values.Length != ExpectedNumberOfArguments)
      {
        throw new InvalidNumberOfArgumentException(
          nameof(IfFunction),
          ExpectedNumberOfArguments,
          values.Length);
      }

      var firstVal = values[0];
      var secondVal = values[1];
      var thirdVal = values[2];

      if (firstVal is null)
      {
        return Task.FromResult(thirdVal);
      }

      if (firstVal is bool boolVal)
      {
        return Task.FromResult(boolVal ? secondVal : thirdVal);
      }

      return Task.FromResult(firstVal.CoerceToBool() ? secondVal : thirdVal);
    }
  }
}