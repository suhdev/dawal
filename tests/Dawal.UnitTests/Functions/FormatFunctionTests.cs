using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class FormatFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new FormatFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, "this is a {0} != {1}" , 100 , 200);
      
      // assert
      result.Should().Be("this is a 100 != 200");
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesWithStringsCorrectly()
    {
      // arrange
      var fn = new FormatFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, "this is a {0} != {1}" , "abc" , "bab");
      
      // assert
      result.Should().Be("this is a abc != bab");
    }
    
    [Fact]
    public async Task ItShouldThrowIfMoreThanTwoParametersArePassed()
    {
      // arrange 
      var fn = new FormatFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}