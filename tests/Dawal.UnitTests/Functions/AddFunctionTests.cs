using System;
using System.Linq;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class AddFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new AddFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10 , 100 , 200, 300);
      
      // assert
      result.Should().Be(new []{10 , 100 , 200, 300}.Sum());
    }
    
    [Fact]
    public async Task ShouldAddTimeSpan()
    {
      // arrange
      var fn = new AddFunction();
      var mock = new Mock<IEvaluationContext>();
      var dateTime = DateTime.UtcNow;
      var ts = TimeSpan.FromDays(10);
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, dateTime, ts);
      
      // assert
      result.Should().Be(dateTime + ts);
    }
    
    [Fact]
    public async Task ShouldAddDateTimeOffsetTimeSpan()
    {
      // arrange
      var fn = new AddFunction();
      var mock = new Mock<IEvaluationContext>();
      var dateTime = DateTimeOffset.UtcNow;
      var ts = TimeSpan.FromDays(10);
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, dateTime, ts);
      
      // assert
      result.Should().Be(dateTime + ts);
    }
    
    [Fact]
    public async Task ShouldConcatenateString()
    {
      // arrange
      var fn = new AddFunction();
      var mock = new Mock<IEvaluationContext>();
      var dateTime = DateTimeOffset.UtcNow;
      var ts = TimeSpan.FromDays(10);
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, "abc", "def");
      
      // assert
      result.Should().Be("abcdef");
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new AddFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}