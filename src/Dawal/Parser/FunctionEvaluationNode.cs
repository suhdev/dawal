using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawal.Parser
{
  internal class FunctionEvaluationNode : IEvaluationNode
  {
    private readonly List<IEvaluationNode> _operands;
    private readonly string _identifier;

    public FunctionEvaluationNode(string identifier, List<IEvaluationNode> operands)
    {
      _identifier = identifier;
      _operands = operands;
    }

    public async Task<object> EvaluateAsync(IEvaluationContext context)
    {
      var values = await Task.WhenAll(_operands.Select(x => x.EvaluateAsync(context)));

      var fn = context.GetFunction(_identifier);

      var result = await fn.ExecuteAsync(context, values);
      return result;
    }

    public async Task<TResult> EvaluateAsync<TResult>(IEvaluationContext context)
    {
      return (TResult)await EvaluateAsync(context);
    }
  }
}