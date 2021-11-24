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
    
    public BaseEvaluationContext(IEnumerable<IEvaluationFunction> functions)
    {
      _functions = functions.ToList();
    }
    
    public IEvaluationFunction GetFunction(string identifier)
    {
      return _functions.First(x =>
        x.GetType().Name.IsEqual(identifier) ||
        x.GetCustomAttribute<EvaluationFunctionAttribute>().MatchIdentifier(identifier));
    }
  }
}