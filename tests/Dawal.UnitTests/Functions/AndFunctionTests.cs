using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class AndFunctionTests
  {
    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(true, null, false)]
    [InlineData(null, null, false)]
    [InlineData(null, true, false)]
    [InlineData(1, true, true)]
    [InlineData(true, 0, false)]
    [InlineData(false, 1, false)]
    [InlineData("", true, false)]
    [InlineData("", "", false)]
    [InlineData(0, "", false)]
    [InlineData(0, "abc", false)]
    [InlineData("abc22", "abc", true)]
    [InlineData("abc22", 122, true)]
    public async Task ShouldEvaluateValuesCorrectly(object val1, object val2, bool expected)
    {
      // arrange
      var fn = new AndFunction();
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
      var fn = new AndFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}