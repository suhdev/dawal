using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class ToNumberFunctionTests
  {
    [Theory]
    [InlineData(12.12, 12.12)]
    [InlineData(false, 0)]
    [InlineData(true, 1)]
    [InlineData("12.12222", 12.12222)]
    [InlineData("    ", 0)]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    public async Task ShouldEvaluateValuesCorrectly(object input, decimal value)
    {
      // arrange
      var fn = new ToNumberFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, input);
      
      // assert
      result.Should().Be(value);
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new ToNumberFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 10, 10));
    }
  }
}