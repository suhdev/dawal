using System;

namespace Dawal.Parser
{
  public class FunctionNotFoundException : Exception
  {
    public string Identifier { get; }

    public FunctionNotFoundException(string identifier)
      : base($"No function registered with identifier '{identifier}'.")
    {
      Identifier = identifier;
    }
  }
}
