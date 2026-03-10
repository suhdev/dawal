using BenchmarkDotNet.Running;

namespace Dawal.Benchmarks
{
  class Program
  {
    static void Main(string[] args) =>
      BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
  }
}
