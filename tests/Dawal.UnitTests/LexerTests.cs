using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Xunit;

namespace Dawal.UnitTests
{
  public class LexerTests
  {
    [Theory]
    [InlineData("EqualTo(10, 10)", true)]
    [InlineData("GreaterThan(10, 5)", true)]
    [InlineData("GreaterThan(10, 20)", false)]
    [InlineData("And(EqualTo(10, 10), LessThan(100 #number#, 1000))", true)]
    [InlineData("And(EqualTo(10, 10), GreaterThan(100, 1000))", false)]
    [InlineData("Not(10)", false)]
    [InlineData("Not('abc')", false)]
    [InlineData("not(Not(''))", false)]
    [InlineData("eq('string', \"string\")", true)]
    public async Task ShouldGenerateValidResult(string program, bool expected)
    {
      // arrange
      var scanner = new Scanner();
      var tokens = scanner.Scan(program);
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var ctx = new BaseEvaluationContext(new IEvaluationFunction[]
      {
        new AndFunction(),
        new OrFunction(),
        new EqualToFunction(),
        new NotFunction(),
        new NotEqualToFunction(),
        new ContextFunction(),
        new IfFunction(),
        new FilterFunction(),
        new FindOneFunction(),
        new GreaterThanFunction(),
        new LessThanFunction(),
        new GreaterThanOrEqualFunction(),
        new LessThanOrEqualFunction(),
        new PropOfFunction()
      });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().Be(expected);
    }

    [Fact]
    public async Task ShouldResolveVariableFromContext()
    {
      // arrange
      var scanner = new Scanner();
      var tokens = scanner.Scan("$count");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { new EqualToFunction() },
        new Dictionary<string, object> { ["count"] = (decimal)42 });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().Be((decimal)42);
    }

    [Fact]
    public async Task ShouldResolveVariableInFunctionCall()
    {
      // arrange
      var scanner = new Scanner();
      var tokens = scanner.Scan("EqualTo($count, 42)");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { new EqualToFunction() },
        new Dictionary<string, object> { ["count"] = (decimal)42 });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().Be(true);
    }

    [Fact]
    public async Task ShouldResolveNestedPropertyViaVariableDotAccess()
    {
      // arrange: $user.name where user = { Name = "Alice" }
      var scanner = new Scanner();
      var tokens = scanner.Scan("EqualTo($user.name, 'Alice')");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var user = new { Name = "Alice" };
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { new EqualToFunction() },
        new Dictionary<string, object> { ["user"] = user });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().Be(true);
    }

    [Fact]
    public async Task ShouldResolveDeepNestedPropertyViaVariableDotAccess()
    {
      // arrange: $order.address.city
      var scanner = new Scanner();
      var tokens = scanner.Scan("EqualTo($order.address.city, 'London')");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var order = new { Address = new { City = "London" } };
      var ctx = new BaseEvaluationContext(
        new IEvaluationFunction[] { new EqualToFunction() },
        new Dictionary<string, object> { ["order"] = order });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().Be(true);
    }

    [Fact]
    public async Task ShouldReturnNullForUndefinedVariable()
    {
      // arrange
      var scanner = new Scanner();
      var tokens = scanner.Scan("$missing");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var ctx = new BaseEvaluationContext(new IEvaluationFunction[] { });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnNullForMemberAccessOnNullVariable()
    {
      // arrange: $missing.prop should return null gracefully
      var scanner = new Scanner();
      var tokens = scanner.Scan("$missing.prop");
      var lexer = new Lexer();
      var rules = lexer.Read(tokens);
      var ctx = new BaseEvaluationContext(new IEvaluationFunction[] { });

      var result = await rules.First().EvaluateAsync(ctx);

      result.Should().BeNull();
    }
  }
}