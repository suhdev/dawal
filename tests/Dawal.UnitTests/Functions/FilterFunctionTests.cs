using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawal.Parser;
using Dawal.Parser.Functions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dawal.UnitTests.Functions
{
  public class FilterFunctionTests
  {
    [Fact]
    public async Task ShouldEvaluateValuesCorrectly()
    {
      // arrange
      var fn = new FilterFunction();
      var mock = new BaseEvaluationContext(new IEvaluationFunction[]
      {
        new EqualToFunction(),
        new ContextFunction(),
        new NotFunction(),
        new NotEqualToFunction(),
        new GreaterThanFunction(),
        new GreaterThanOrEqualFunction(),
        new LessThanFunction(),
        new PropOfFunction()
      });
      var someList = new List<CustomObject>()
      {
        new CustomObject()
        {
          FirstName = "John",
          LastName = "Doe",
          Age = 33
        },
        new CustomObject()
        {
          FirstName = "James",
          LastName = "Gunn",
          Age = 44
        },
        new CustomObject()
        {
          FirstName = "Jake",
          LastName = "Watts",
          Age = 65
        }
      };
      
      // act 
      var result = await fn.ExecuteAsync(mock, 
        someList, "age", "lt", 45);
      
      // assert
      result.Should().BeEquivalentTo(someList.Where(x => x.Age < 45));
    }
    
    [Fact]
    public async Task ShouldEvaluateValuesCorrectlyWithoutFunctionNames()
    {
      // arrange
      var fn = new FilterFunction();
      var mock = new BaseEvaluationContext(new IEvaluationFunction[]
      {
        new EqualToFunction(),
        new ContextFunction(),
        new NotFunction(),
        new NotEqualToFunction(),
        new GreaterThanFunction(),
        new GreaterThanOrEqualFunction(),
        new LessThanFunction(),
        new PropOfFunction()
      });
      var someList = new List<CustomObject>()
      {
        new CustomObject()
        {
          FirstName = "John",
          LastName = "Doe",
          Age = 33
        },
        new CustomObject()
        {
          FirstName = "James",
          LastName = "Gunn",
          Age = 33
        },
        new CustomObject()
        {
          FirstName = "Jake",
          LastName = "Watts",
          Age = 65
        }
      };
      
      // act 
      var result = await fn.ExecuteAsync(mock, 
        someList, "age", 33);
      
      // assert
      result.Should().BeEquivalentTo(someList.Where(x => x.Age == 33));
    }
    
    [Fact]
    public async Task ItShouldThrowIfInvalidNumberOfArgumentsArePassed()
    {
      // arrange 
      var fn = new FilterFunction();
      var mock = new Mock<IEvaluationContext>();
      
      // act & assert
      await Assert.ThrowsAsync<InvalidNumberOfArgumentException>(async () => await fn.ExecuteAsync(mock.Object, 10));
    }
  }
}