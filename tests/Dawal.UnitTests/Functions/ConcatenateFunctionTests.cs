using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class ConcatenateFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new ConcatenateFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, "abc", "a", "b", "c");
      
      // assert
      result.Should().Be("abcabc");
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new ConcatenateFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}