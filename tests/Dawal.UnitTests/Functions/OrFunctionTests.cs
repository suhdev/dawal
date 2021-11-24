using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class OrFunctionTests
  {
    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, true)]
    [InlineData(true, null, true)]
    [InlineData(null, null, false)]
    [InlineData(null, true, true)]
    [InlineData(1, true, true)]
    [InlineData(true, 0, true)]
    [InlineData(false, 1, true)]
    [InlineData("", true, true)]
    [InlineData("", "", false)]
    [InlineData(0, "", false)]
    [InlineData(0, "abc", true)]
    [InlineData("abc22", "abc", true)]
    [InlineData("abc22", 122, true)]
    public async Task ShouldEvaluateValuesCorrectly(object val1, object val2, bool expected)
    {
      // arrange
      var fn = new OrFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, val1, val2);
      
      // assert
      result.Should().Be(expected);
    }
    
    [Fact]
    public async Task ItShouldThrowIfMoreThanTwoParametersArePassed()
    {
      // arrange 
      var fn = new OrFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}