using System.Text.RegularExpressions;

namespace Dawal.Parser
{
  public class TokenRegex
  {
    public static Regex Whitespace { get; } = new Regex("^[\\s]+");
    public static Regex Comment { get; } = new Regex("^#[^#]*?#");
    public static Regex Number { get; } = new Regex("^\\-?[0-9]+(\\.[0-9]+)?");
    public static Regex Null { get; } = new Regex("^null", RegexOptions.IgnoreCase);
    public static Regex Bool { get; } = new Regex("^(true|false)", RegexOptions.IgnoreCase);
    public static Regex String { get; } = new Regex("(^'[^']*?')|(^\"[^\"]*?\")");
    public static Regex LParen { get; } = new Regex("^\\(");
    public static Regex RParen { get; } = new Regex("^\\)");
    public static Regex Comma { get; } = new Regex("^,");
    public static Regex Identifier { get; } = new Regex("^[a-z][a-z0-9_]*", RegexOptions.IgnoreCase);
  }
}