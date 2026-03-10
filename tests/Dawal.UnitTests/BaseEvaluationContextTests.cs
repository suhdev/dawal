using System.Collections.Generic;
using Dawal.Parser;
using Dawal.Parser.Functions;
using Xunit;

namespace Dawal.UnitTests
{
  public class BaseEvaluationContextTests
  {
    [Fact]
    public void GetFunction_ShouldThrowFunctionNotFoundException_WhenIdentifierIsUnknown()
    {
      // arrange
      var ctx = new BaseEvaluationContext(new IEvaluationFunction[]
      {
        new EqualToFunction()
      });

      // act & assert
      var ex = Assert.Throws<FunctionNotFoundException>(() => ctx.GetFunction("does_not_exist"));
      Assert.Equal("does_not_exist", ex.Identifier);
    }

    [Fact]
    public void GetFunction_ShouldResolveByAlias()
    {
      // arrange
      var ctx = new BaseEvaluationContext(new IEvaluationFunction[]
      {
        new EqualToFunction()
      });

      // act
      var fn = ctx.GetFunction("eq");

      // assert
      Assert.IsType<EqualToFunction>(fn);
    }

    [Fact]
    public void GetVariable_ShouldReturnNull_WhenVariableIsNotRegistered()
    {
      // arrange
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { },
        new Dictionary<string, object> { ["known"] = 42 });

      // act
      var result = ctx.GetVariable("unknown");

      // assert
      Assert.Null(result);
    }

    [Fact]
    public void GetVariable_ShouldReturnValue_WhenVariableIsRegistered()
    {
      // arrange
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { },
        new Dictionary<string, object> { ["count"] = (decimal)7 });

      // act
      var result = ctx.GetVariable("count");

      // assert
      Assert.Equal((decimal)7, result);
    }
  }
}
