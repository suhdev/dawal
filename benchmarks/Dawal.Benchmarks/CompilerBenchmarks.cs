using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Dawal.Parser;
using Dawal.Parser.Functions;

namespace Dawal.Benchmarks
{
  [MemoryDiagnoser]
  public class CompilerBenchmarks
  {
    private readonly DawalCompiler _compiler = new DawalCompiler();

    private IEvaluationContext _ctx;
    private IEvaluationContext _ctxWithVariables;

    // Pre-compiled nodes for evaluation-only benchmarks
    private IEvaluationNode _equalToNode;
    private IEvaluationNode _nestedNode;
    private IEvaluationNode _variableNode;
    private IEvaluationNode _variableMemberNode;
    private IEvaluationNode _variableInFunctionNode;

    private static readonly object UserData = new { Role = "admin", Name = "Alice" };
    private static readonly object OrderData = new { Address = new { City = "London" } };

    [GlobalSetup]
    public void Setup()
    {
      var functions = new IEvaluationFunction[]
      {
        new EqualToFunction(),
        new AndFunction(),
        new OrFunction(),
        new NotFunction(),
        new GreaterThanFunction(),
        new LessThanFunction(),
        new PropOfFunction(),
      };

      var variables = new Dictionary<string, object>
      {
        ["user"]  = UserData,
        ["order"] = OrderData,
      };

      _ctx = new BaseEvaluationContext(functions);
      _ctxWithVariables = new BaseEvaluationContext(functions, variables);

      _equalToNode          = _compiler.Compile("EqualTo(10, 10)").First();
      _nestedNode           = _compiler.Compile("And(EqualTo(10, 10), LessThan(100, 1000))").First();
      _variableNode         = _compiler.Compile("$user").First();
      _variableMemberNode   = _compiler.Compile("$order.address.city").First();
      _variableInFunctionNode = _compiler.Compile("EqualTo($user.role, 'admin')").First();
    }

    // ── Compile (scan + parse) ────────────────────────────────────────────────

    [Benchmark(Description = "Compile: EqualTo(10, 10)")]
    public object CompileSimple() => _compiler.Compile("EqualTo(10, 10)");

    [Benchmark(Description = "Compile: nested And/LessThan")]
    public object CompileNested() =>
      _compiler.Compile("And(EqualTo(10, 10), LessThan(100, 1000))");

    [Benchmark(Description = "Compile: variable reference")]
    public object CompileVariable() => _compiler.Compile("$user");

    [Benchmark(Description = "Compile: variable with member access")]
    public object CompileVariableMemberAccess() => _compiler.Compile("$order.address.city");

    [Benchmark(Description = "Compile: variable in function call")]
    public object CompileVariableInFunction() => _compiler.Compile("EqualTo($user.role, 'admin')");

    // ── Evaluate (pre-compiled AST) ───────────────────────────────────────────

    [Benchmark(Description = "Evaluate: EqualTo(10, 10)")]
    public Task<object> EvaluateSimple() => _equalToNode.EvaluateAsync(_ctx);

    [Benchmark(Description = "Evaluate: nested And/LessThan")]
    public Task<object> EvaluateNested() => _nestedNode.EvaluateAsync(_ctx);

    [Benchmark(Description = "Evaluate: variable reference")]
    public Task<object> EvaluateVariable() => _variableNode.EvaluateAsync(_ctxWithVariables);

    [Benchmark(Description = "Evaluate: variable with member access")]
    public Task<object> EvaluateVariableMemberAccess() =>
      _variableMemberNode.EvaluateAsync(_ctxWithVariables);

    [Benchmark(Description = "Evaluate: variable in function call")]
    public Task<object> EvaluateVariableInFunction() =>
      _variableInFunctionNode.EvaluateAsync(_ctxWithVariables);

    // ── Full pipeline (compile + evaluate) ───────────────────────────────────

    [Benchmark(Description = "Full pipeline: EqualTo(10, 10)")]
    public async Task<object> FullPipelineSimple()
    {
      var node = _compiler.Compile("EqualTo(10, 10)").First();
      return await node.EvaluateAsync(_ctx);
    }

    [Benchmark(Description = "Full pipeline: variable in function call")]
    public async Task<object> FullPipelineVariableInFunction()
    {
      var node = _compiler.Compile("EqualTo($user.role, 'admin')").First();
      return await node.EvaluateAsync(_ctxWithVariables);
    }
  }
}
