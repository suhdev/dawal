using System;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class TimeSpanFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesWithoutSecondsCorrectly()
    {
      // arrange
      var fn = new TimeSpanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10, 20, 10);
      
      // assert
      var ts = (TimeSpan)result;
      ts.Should().Be(new TimeSpan(10, 20, 10, 0));
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesWithoutMinutesCorrectly()
    {
      // arrange
      var fn = new TimeSpanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10, 20);
      
      // assert
      var ts = (TimeSpan)result;
      ts.Should().Be(new TimeSpan(10, 20, 0, 0));
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesWithoutHoursCorrectly()
    {
      // arrange
      var fn = new TimeSpanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10);
      
      // assert
      var ts = (TimeSpan)result;
      ts.Should().Be(new TimeSpan(10, 0, 0, 0));
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new TimeSpanFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}