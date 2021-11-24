using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class PropOfFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new PropOfFunction();
      var mock = new Mock<IEvaluationContext>();
      var arg = new { a = new { b = new { firstName = "John" } } };
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object,arg, "a");

      // assert
      result.Should().Be(arg.a);
    }
    
    [Fact]
    public async Task ShouldEvaluateNestedValuesCorrectly()
    {
      // arrange
      var fn = new PropOfFunction();
      var mock = new Mock<IEvaluationContext>();
      var arg = new { a = new { b = new { firstName = "John" } } };
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object,arg, "a");
      var result2 = await fn.ExecuteAsync(mock.Object, result, "b");
      var firstName = await fn.ExecuteAsync(mock.Object, result2, "firstName");

      // assert
      firstName.Should().Be("John");
    }
    
    [Theory]
    [InlineData("firstName")]
    [InlineData("FirstName")]
    [InlineData("First_Name")]
    [InlineData("first_name")]
    [InlineData("firstname")]
    public async Task ShouldEvaluateNestedValuesCorrectlyWithFlexiblePropName(string propName)
    {
      // arrange
      var fn = new PropOfFunction();
      var mock = new Mock<IEvaluationContext>();
      var arg = new { a = new { b = new { firstName = "John" } } };
      
      // act 
      var result = await fn.ExecuteAsync(mock.Object,arg, "a");
      var result2 = await fn.ExecuteAsync(mock.Object, result, "b");
      var firstName = await fn.ExecuteAsync(mock.Object, result2, propName);

      // assert
      firstName.Should().Be("John");
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