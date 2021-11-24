using System;
using System.Linq;

namespace Dawal.Parser.Functions
{
  [AttributeUsage(AttributeTargets.Class)]
  public class EvaluationFunctionAttribute : Attribute
  {
    public string[] Names { get; }

    public EvaluationFunctionAttribute(params string[] names)
    {
      Names = names;
    }

    public bool MatchIdentifier(string identifier)
    {
      return Names.Any(x => x.IsEqual(identifier));
    }
  }
}