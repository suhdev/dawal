using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class NotEqualFunctionTests
  {
    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(1, 0, true)]
    [InlineData("a", 0, true)]
    [InlineData("a", "a", false)]
    [InlineData(true, "a", true)]
    [InlineData(true, true, false)]
    [InlineData(false, false, false)]
    [InlineData(false, true, true)]
    [InlineData(10.1, true, true)]
    [InlineData(10.1, 10.1, false)]
    public async Task ShouldEvaluateValuesCorrectly(object firstOperand, object secondOperand, bool expected)
    {
      // arrange
      var fn = new NotEqualToFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, firstOperand, secondOperand);
      
      // assert
      result.Should().Be(expected);
    }
    
    [Fact]
    public async Task ItShouldThrowIfMoreThanTwoParametersArePassed()
    {
      // arrange 
      var fn = new NotEqualToFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100, 100));
    }
  }
}