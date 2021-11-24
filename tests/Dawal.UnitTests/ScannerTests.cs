using System;
using System.Linq;
using Dawal.Parser;
using FluentAssertions;
using Xunit;

namespace Dawal.UnitTests
{
  public class ScannerTests
  {
    [Theory]
    [InlineData("10", 1)]
    [InlineData("10.1", 1)]
    [InlineData("TRUE", 1)]
    [InlineData("true", 1)]
    [InlineData("false", 1)]
    [InlineData("FALSE", 1)]
    [InlineData("NULL", 1)]
    [InlineData("null", 1)]
    [InlineData("'abc'", 1)]
    [InlineData("(", 1)]
    [InlineData(")", 1)]
    [InlineData(",", 1)]
    [InlineData("     ", 0)]
    [InlineData("()", 2)]
    [InlineData("EQ(10, 10)", 6)]
    [InlineData("EQ(null, 10)", 6)]
    [InlineData("AND(null, 10)", 6)]
    [InlineData("AND(null, TRUE)", 6)]
    [InlineData("AND(null, FALSE)", 6)]
    public void ItShouldScanTokensCorrectly(string program, int tokenCount)
    {
      var scanner = new Scanner();
      var tokens = scanner.Scan(program);
      tokens.Count.Should().Be(tokenCount);
    }
    
    [Theory]
    [InlineData("10", TokenType.Number)]
    [InlineData("10.1", TokenType.Number)]
    [InlineData("TRUE", TokenType.Boolean)]
    [InlineData("true", TokenType.Boolean)]
    [InlineData("false", TokenType.Boolean)]
    [InlineData("FALSE", TokenType.Boolean)]
    [InlineData("NULL", TokenType.Null)]
    [InlineData("null", TokenType.Null)]
    [InlineData("'abc'", TokenType.String)]
    [InlineData("(", TokenType.LParen)]
    [InlineData(")", TokenType.RParen)]
    [InlineData(",", TokenType.Comma)]
    [InlineData("AND", TokenType.Identifier)]
    public void ItShouldScanTokensCorrectlyWithTypes(string program, TokenType tokenType)
    {
      var scanner = new Scanner();
      var tokens = scanner.Scan(program);
      tokens.First().TokenType.Should().Be(tokenType);
    }
    
    [Theory]
    [InlineData("!12")]
    [InlineData("@233")]
    [InlineData("'22")]
    [InlineData("33'")]
    [InlineData("'false")]
    [InlineData("[")]
    [InlineData("[NULL]")]
    public void ItShouldThrowOnInvalidCharacterStream(string program)
    {
      var scanner = new Scanner();

      Assert.Throws<Exception>(() => scanner.Scan(program));
    }
  }
}