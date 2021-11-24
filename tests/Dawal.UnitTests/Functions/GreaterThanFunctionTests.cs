using System;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class GreaterThanFunctionTests
  {
    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(1, 0, true)]
    [InlineData("a", 0, false)]
    [InlineData("a", "a", false)]
    [InlineData(true, "a", false)]
    [InlineData(true, true, false)]
    [InlineData(false, false, false)]
    [InlineData(true, false, true)]
    [InlineData(false, true, false)]
    [InlineData(10.1, true, false)]
    [InlineData(10.1, 10.1, false)]
    [InlineData(10.1, 10.2, false)]
    [InlineData(100.1, 10.2, true)]
    [InlineData("bab", "aba", true)]
    [InlineData("abab", "baba", false)]
    public async Task ShouldEvaluateValuesCorrectly(object firstOperand, object secondOperand, bool expected)
    {
      // arrange
      var fn = new GreaterThanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, firstOperand, secondOperand);
      
      // assert
      result.Should().Be(expected);
    }
    
    [Fact]
    public async Task ShouldEvaluateDateValuesCorrectly()
    {
      // arrange
      var fn = new GreaterThanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 
        DateTimeOffset.UtcNow.AddDays(100), 
        DateTimeOffset.UtcNow.AddDays(10));
      var result2 = await fn.ExecuteAsync(mock.Object, 
        DateTimeOffset.UtcNow.AddDays(10), 
        DateTimeOffset.UtcNow.AddDays(100));
      
      // assert
      result.Should().Be(true);
      result2.Should().Be(false);
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new EqualToFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100, 100));
    }
  }
}