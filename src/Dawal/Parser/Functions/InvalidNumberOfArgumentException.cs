using System;

namespace Dawal.Parser.Functions
{
  public class InvalidNumberOfArgumentException : Exception
  {
    public InvalidNumberOfArgumentException(
      string fn,
      int expectedNumberOfArguments, int receivedNumberOfArguments) 
      : base($"Expected '{expectedNumberOfArguments}' operands for function '${fn}' but got '{receivedNumberOfArguments}' operands instead")
    {
    }
  }
}