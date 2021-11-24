using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class BetweenFunctionTests
  {
    [Theory]
    [InlineData(0, 0, 1000, true)]
    [InlineData(1000, 0, 1000, true)]
    [InlineData(50, 0, 1000, true)]
    [InlineData(50, 100, 1000, false)]
    [InlineData(50, 1000, 1000, false)]
    [InlineData(50, 0, 0, false)]
    public async Task ShouldEvaluateValuesCorrectly(
      object val, object min, object max, bool expected)
    {
      // arrange
      var fn = new BetweenFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, val, min, max);
      
      // assert
      result.Should().Be(expected);
    }
    
    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new BetweenFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100, 100));
    }
  }
}