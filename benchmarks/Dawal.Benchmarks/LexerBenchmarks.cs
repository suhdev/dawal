using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Dawal.Parser;
using Dawal.Parser.Functions;

namespace Dawal.Benchmarks
{
  [MemoryDiagnoser]
  public class LexerBenchmarks
  {
    private readonly Scanner _scanner = new Scanner();
    private readonly Lexer _lexer = new Lexer();

    private List<Token> _simpleFunctionTokens;
    private List<Token> _nestedFunctionTokens;
    private List<Token> _deeplyNestedTokens;
    private List<Token> _variableTokens;
    private List<Token> _variableMemberAccessTokens;
    private List<Token> _variableInFunctionTokens;

    [GlobalSetup]
    public void Setup()
    {
      _simpleFunctionTokens = _scanner.Scan("EqualTo(10, 10)");
      _nestedFunctionTokens = _scanner.Scan("And(EqualTo(10, 10), LessThan(100, 1000))");
      _deeplyNestedTokens   = _scanner.Scan("And(EqualTo(10, 10), Or(LessThan(100, 1000), GreaterThan(5, 1)))");
      _variableTokens       = _scanner.Scan("$myVar");
      _variableMemberAccessTokens = _scanner.Scan("$user.name");
      _variableInFunctionTokens   = _scanner.Scan("EqualTo($user.role, 'admin')");
    }

    [Benchmark(Description = "Parse: simple function call")]
    public object ParseSimpleFunction() => _lexer.Read(_simpleFunctionTokens);

    [Benchmark(Description = "Parse: nested function call")]
    public object ParseNestedFunction() => _lexer.Read(_nestedFunctionTokens);

    [Benchmark(Description = "Parse: deeply nested function calls")]
    public object ParseDeeplyNested() => _lexer.Read(_deeplyNestedTokens);

    [Benchmark(Description = "Parse: variable reference")]
    public object ParseVariable() => _lexer.Read(_variableTokens);

    [Benchmark(Description = "Parse: variable with member access")]
    public object ParseVariableMemberAccess() => _lexer.Read(_variableMemberAccessTokens);

    [Benchmark(Description = "Parse: variable in function call")]
    public object ParseVariableInFunction() => _lexer.Read(_variableInFunctionTokens);
  }
}
