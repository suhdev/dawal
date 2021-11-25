using System.Collections.Generic;

namespace Dawal.Parser
{
  public class DawalCompiler
  {
    public List<IEvaluationNode> Compile(string program)
    {
      var scanner = new Scanner();
      var tokens = scanner.Scan(program);
      var lexer = new Lexer();
      return lexer.Read(tokens);
    }
  }
}