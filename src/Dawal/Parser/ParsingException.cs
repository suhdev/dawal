using System;

namespace Dawal.Parser
{
  public class ParsingException : Exception
  {
    public int Position { get; }

    public ParsingException(int position, string script)
      : base($"Unexpected character at position {position} in: '{script}'")
    {
      Position = position;
    }
  }
}
