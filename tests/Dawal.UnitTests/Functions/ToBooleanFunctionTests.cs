using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class ToBooleanFunctionTests
  {
    [Theory]
    [InlineData(12.12, true)]
    [InlineData(false, false)]
    [InlineData(true, true)]
    [InlineData("12.12222", true)]
    [InlineData("    ", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public async Task ShouldEvaluateValuesCorrectly(object input, bool value)
    {
      // arrange
      var fn = new ToBooleanFunction();
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