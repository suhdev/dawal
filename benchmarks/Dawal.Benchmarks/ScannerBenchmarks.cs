using BenchmarkDotNet.Attributes;
using Dawal.Parser;

namespace Dawal.Benchmarks
{
  [MemoryDiagnoser]
  public class ScannerBenchmarks
  {
    private readonly Scanner _scanner = new Scanner();

    [Benchmark(Description = "Scan: single literal")]
    public object ScanSingleNumber() => _scanner.Scan("42");

    [Benchmark(Description = "Scan: simple function call")]
    public object ScanSimpleFunction() => _scanner.Scan("EqualTo(10, 10)");

    [Benchmark(Description = "Scan: nested function call")]
    public object ScanNestedFunction() =>
      _scanner.Scan("And(EqualTo(10, 10), LessThan(100, 1000))");

    [Benchmark(Description = "Scan: expression with comment")]
    public object ScanWithComment() =>
      _scanner.Scan("EqualTo(100, 1000 #this is a comment#)");

    [Benchmark(Description = "Scan: variable reference")]
    public object ScanVariable() => _scanner.Scan("$myVar");

    [Benchmark(Description = "Scan: variable with member access")]
    public object ScanVariableWithMemberAccess() => _scanner.Scan("$user.name");

    [Benchmark(Description = "Scan: deep member access chain")]
    public object ScanDeepMemberAccess() => _scanner.Scan("$order.address.city");

    [Benchmark(Description = "Scan: variable in function call")]
    public object ScanVariableInFunction() => _scanner.Scan("EqualTo($user.role, 'admin')");
  }
}
