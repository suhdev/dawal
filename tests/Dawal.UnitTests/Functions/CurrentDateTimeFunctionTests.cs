using System;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class CurrentDateTimeFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new CurrentDateTimeFunction();
      var mock = new Mock<IEvaluationContext>();
      var currentDate = DateTimeOffset.UtcNow;
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object);
      
      // assert
      var year = result.CoerceToDateTime().Year;
      var month = result.CoerceToDateTime().Month;
      var day = result.CoerceToDateTime().Day;
      var hour = result.CoerceToDateTime().Hour;
      var minute = result.CoerceToDateTime().Minute;
      var second = result.CoerceToDateTime().Second;

      year.Should().Be(currentDate.Year);
      month.Should().Be(currentDate.Month);
      day.Should().Be(currentDate.Day);
      hour.Should().Be(currentDate.Hour);
      minute.Should().Be(currentDate.Minute);
      second.Should().Be(currentDate.Second);
    }
    
    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new CurrentDateTimeFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10, 100, 100, 100));
    }
  }
}