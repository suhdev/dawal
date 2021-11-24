using System;
using System.Collections.Generic;
using System.Linq;
using CaseExtensions;

namespace Dawal.Parser
{
  public class Lexer
  {
    private Queue<Token> _tokens;
    public List<IEvaluationNode> Read(List<Token> tokens)
    {
      _tokens = new Queue<Token>(tokens);
      var result = new List<IEvaluationNode>();

      while (_tokens.Count > 0)
      {
        var expression = ReadFunction();

        result.Add(expression);
      }

      return result;
    }

    private IEvaluationNode ReadFunction()
    {
      var identifier = ReadIdentifier();
      var operands = new List<IEvaluationNode>();
      ReadLParen();
      while (!IsNextToken(TokenType.RParen))
      {
        var operand = ReadOperand();
        operands.Add(operand);

        if (IsNextToken(TokenType.Comma))
        {
          ReadComma();
        }
      }
      ReadRParen();
      return new FunctionEvaluationNode(identifier, operands);
    }

    private IEvaluationNode ReadOperand()
    {
      if (IsNextToken(TokenType.Identifier))
      {
        return ReadFunction();
      }

      if (IsNextToken(TokenType.Boolean, TokenType.Number, TokenType.Null, TokenType.String))
      {
        return ReadValue();
      }

      throw new Exception($"Expected primitive token but got {Peek().TokenType}");
    }

    private IEvaluationNode ReadValue()
    {
      if (IsNextToken(TokenType.Null))
      {
        SkipToken(TokenType.Null);
        return new EvaluationNodeValue<object>(null);
      }

      if (IsNextToken(TokenType.Boolean))
      {
        return new EvaluationNodeValue<bool>(ReadBoolean());
      }

      if (IsNextToken(TokenType.Number))
      {
        return new EvaluationNodeValue<decimal>(ReadNumber());
      }

      if (IsNextToken(TokenType.String))
      {
        return new EvaluationNodeValue<string>(ReadString());
      }

      throw new Exception($"Expected primitive token but got {Peek().TokenType}");
    }

    private string ReadString()
    {
      return ReadToken(TokenType.String).Value.Trim('\'');
    }

    private decimal ReadNumber()
    {
      return decimal.Parse(ReadToken(TokenType.Number).Value);
    }

    private bool ReadBoolean()
    {
      return ReadToken(TokenType.Boolean).Value.ToLower().Equals("true");
    }

    private void ReadComma()
    {
      SkipToken(TokenType.Comma);
    }

    private bool IsNextToken(params TokenType[] tokenTypes)
    {
      return tokenTypes.Contains(Peek().TokenType);
    }

    private void ReadRParen()
    {
      SkipToken(TokenType.RParen);
    }

    private Token Peek()
    {
      return _tokens.Peek();
    }

    private void ReadLParen()
    {
      SkipToken(TokenType.LParen);
    }

    private string ReadIdentifier()
    {
      var token = ReadToken(TokenType.Identifier);

      return token.Value.ToSnakeCase();
    }

    private void SkipToken(TokenType type)
    {
      ReadToken(type);
    }

    private Token ReadToken(TokenType type)
    {
      var token = _tokens.Dequeue();
      if (token.TokenType != type)
      {
        throw new Exception($"Expected {type} but got {token.TokenType} instead");
      }

      return token;
    }
  }
}