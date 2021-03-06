using System;
using System.Threading.Tasks;

namespace Dawal.Parser
{
  public class EvaluationNodeValue<TValue> : IEvaluationNode
  {
    
    public TValue Value { get; }

    public EvaluationNodeValue(TValue value)
    {
      Value = value;
    }
    
    public async Task<object> EvaluateAsync(IEvaluationContext context)
    {
      return Value;
    }

    public async Task<TResult> EvaluateAsync<TResult>(IEvaluationContext context)
    {
      return (TResult) await EvaluateAsync(context);
    }

    public override string ToString()
    {
      return Value is string strValue? CreateStringValue(strValue) : $"{Value.ToString()}";
    }
    
    private string CreateStringValue(string value)
    {
      if (value == null)
      {
        return "null";
      }

      if (value.Contains("'"))
      {
        return $"\"{value}\"";
      }

      return $"'{value}'";
    }
  }
}