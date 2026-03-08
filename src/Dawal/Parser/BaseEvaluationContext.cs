using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dawal.Parser.Functions;

namespace Dawal.Parser
{
  public class BaseEvaluationContext : IEvaluationContext
  {
    public CancellationToken CancellationToken { get; set; }
    protected readonly List<IEvaluationFunction> _functions;
    private readonly IDictionary<string, object> _variables;
    
    public BaseEvaluationContext(IEnumerable<IEvaluationFunction> functions, IDictionary<string, object> variables = null)
    {
      _functions = functions.ToList();
      _variables = variables ?? new Dictionary<string, object>();
    }
    
    public IEvaluationFunction GetFunction(string identifier)
    {
      return _functions.First(x =>
        x.GetType().Name.IsEqual(identifier) ||
        x.GetCustomAttribute<EvaluationFunctionAttribute>().MatchIdentifier(identifier));
    }

    public object GetVariable(string name)
    {
      return _variables.TryGetValue(name, out var value) ? value : null;
    }
  }
}