using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class IfFunctionTests
  {
    [Theory]
    [InlineData(true, "a", "b", "a")]
    [InlineData(false, "a", "b", "b")]
    [InlineData(false, 1, 2, 2)]
    [InlineData(true, 1, 2, 1)]
    [InlineData(true, true, false, true)]
    [InlineData(true, "a", false, "a")]
    [InlineData(true, 12.1, false, 12.1)]
    [InlineData(false, 12.1, false, false)]
    public async Task ShouldEvaluateValuesCorrectly(bool condition, object val1, object val2, object output)
    {
      // arrange
      var fn = new IfFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, condition, val1, val2);
      
      // assert
      result.Should().Be(output);
    }
    
    [Fact]
    public async Task ItShouldThrowIfMoreThanTwoParametersArePassed()
    {
      // arrange 
      var fn = new EqualToFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100, 100));
    }
  }
}