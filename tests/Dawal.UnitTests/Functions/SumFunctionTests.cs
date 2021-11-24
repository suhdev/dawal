using System.Linq;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class SumFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new SumFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10 , 100 , 200, 300);
      
      // assert
      result.Should().Be(new []{10 , 100 , 200, 300}.Sum());
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesCorrectlyWithMixOfValueTypes()
    {
      // arrange
      var fn = new SumFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10 , false , 200, 300, "12.22");
      
      // assert
      result.Should().Be(new decimal[]{10 , 0 , 200, 300, (decimal)12.22}.Sum());
    }
    
    [Fact]
    public async Task ItShouldThrowIfMoreThanTwoParametersArePassed()
    {
      // arrange 
      var fn = new SumFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
  
  public class AverageFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new AverageFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10 , 100 , 200, 300);
      
      // assert
      result.Should().Be(new []{10 , 100 , 200, 300}.Average());
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesCorrectlyWithMixOfValueTypes()
    {
      // arrange
      var fn = new AverageFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 10 , false , 200, 300, "12.22");
      
      // assert
      result.Should().Be(new decimal[]{10 , 0 , 200, 300, (decimal)12.22}.Average());
    }
    
    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new AverageFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}