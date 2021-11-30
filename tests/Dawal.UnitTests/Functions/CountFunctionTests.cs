using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class CountFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new CountFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, new decimal[]{12, 12, 44, 55});
      
      // assert
      result.Should().Be(4);
    }
    
    [Fact]
    public async Task ShouldEvaluateStringArrayValuesCorrectly()
    {
      // arrange
      var fn = new CountFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, (object)new string[]{"12","12", "44", "55"});
      
      // assert
      result.Should().Be(4);
    }
    
    [Fact]
    public async Task ShouldEvaluateComboOfValuesArrayValuesCorrectly()
    {
      // arrange
      var fn = new CountFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, (object)new object[]{"12",12, true, "55"});
      
      // assert
      result.Should().Be(4);
    }
    
    [Fact]
    public async Task ShouldEvaluateStringValuesCorrectly()
    {
      // arrange
      var fn = new CountFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, "abc123");
      
      // assert
      result.Should().Be(6);
    }
    
    [Fact]
    public async Task ShouldEvaluateNumberValuesCorrectly()
    {
      // arrange
      var fn = new CountFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object, 4554);
      
      // assert
      result.Should().Be(4554);
    }

    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new MaxFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object));
    }
  }
}