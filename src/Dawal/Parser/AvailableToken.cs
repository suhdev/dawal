using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dawal.Parser
{
  public class AvailableToken
  {
    public Regex Regex { get; }
    public TokenType TokenType { get; }

    public AvailableToken(TokenType tokenType, Regex regex)
    {
      Regex = regex;
      TokenType = tokenType;
    }


    public static List<AvailableToken> Tokens { get; } = new List<AvailableToken>
    {
      new AvailableToken(TokenType.Null, TokenRegex.Null),
      new AvailableToken(TokenType.Whitespace, TokenRegex.Whitespace),
      new AvailableToken(TokenType.Comment, TokenRegex.Comment),
      new AvailableToken(TokenType.Boolean, TokenRegex.Bool),
      new AvailableToken(TokenType.Number, TokenRegex.Number),
      new AvailableToken(TokenType.String, TokenRegex.String),
      new AvailableToken(TokenType.Comma, TokenRegex.Comma),
      new AvailableToken(TokenType.LParen, TokenRegex.LParen),
      new AvailableToken(TokenType.RParen, TokenRegex.RParen),
      new AvailableToken(TokenType.Identifier, TokenRegex.Identifier)
    };
  }
}