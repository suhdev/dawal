using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class NotFunctionTests
  {
    [Theory]
    [InlineData(1, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(null, true)]
    [InlineData("", true)]
    public async Task ShouldEvaluateValueCorrectly(object value, bool expected)
    {
      // arrange 
      var fn = new NotFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act
      var result = await fn.ExecuteAsync(mock.Object, value);
      
      // assert
      result.Should().Be(expected);
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new NotFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100));
    }
    
  }
}