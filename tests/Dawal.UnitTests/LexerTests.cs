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
  }
}