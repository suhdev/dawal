using System.Threading;

namespace Dawal.Parser
{
  public interface IEvaluationContext
  {
    CancellationToken CancellationToken { get; set; }
    IEvaluationFunction GetFunction(string identifier);
  }

  public class Question
  {
    public Answer Answer { get; set; }
  }

  public class Answer
  {
    public decimal Number { get; set; }
    public bool Boolean { get; set; }
  }
}