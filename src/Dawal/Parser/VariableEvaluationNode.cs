using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawal.Parser.Functions;

namespace Dawal.Parser
{
  public class VariableEvaluationNode : IEvaluationNode
  {
    private readonly string _name;
    private readonly IReadOnlyList<string> _path;

    public VariableEvaluationNode(string name, IEnumerable<string> path = null)
    {
      _name = name;
      _path = (path ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
    }

    public Task<object> EvaluateAsync(IEvaluationContext context)
    {
      var value = context.GetVariable(_name);

      foreach (var property in _path)
      {
        if (value == null)
        {
          return Task.FromResult<object>(null);
        }

        value = value.GetProperty(property);
      }

      return Task.FromResult(value);
    }

    public async Task<TResult> EvaluateAsync<TResult>(IEvaluationContext context)
    {
      return (TResult)await EvaluateAsync(context);
    }

    public override string ToString()
    {
      return _path.Count > 0
        ? $"${_name}.{string.Join(".", _path)}"
        : $"${_name}";
    }
  }
}
