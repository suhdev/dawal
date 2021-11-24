using System;
using System.Collections.Generic;

namespace Dawal.Parser
{
  public class Scanner
  {
    private int _position;
    private string _script;
    public List<Token> Scan(string script)
    {
      _script = script;
      _position = 0;
      var queue = new List<Token>();

      while (_position < script.Length)
      {
        var token = Peek();
        if (token == null)
        {
          throw new Exception("Invalid script");
        }
        
        _position += token.Value.Length;
        if (token.TokenType == TokenType.Whitespace || token.TokenType == TokenType.Comment)
        {
          continue;
        }
        
        queue.Add(token);
      }

      return queue;
    }

    private Token Peek()
    {
      foreach (var token in AvailableToken.Tokens)
      {
        var match = token.Regex.Match(_script.Substring(_position));
        if (match.Success)
        {
          return new Token { TokenType = token.TokenType, Value = match.Value };
        }
      }

      return null;
    }
  }
}