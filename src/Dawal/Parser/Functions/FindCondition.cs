using System.Collections.Generic;

namespace Dawal.Parser.Functions
{
  internal class FindCondition
  {
    public string FunctionName { get; set; }
    public string PropName { get; set; }
    public object Value { get; set; }
  }

  internal static class FindConditionParser
  {
    /// <summary>
    /// Parses the variadic parameters after the list argument into a list of conditions.
    /// <para>
    /// Accepts two calling conventions:
    /// <list type="bullet">
    ///   <item>3-tuple style: prop, functionName, value (repeating)</item>
    ///   <item>2-tuple style: prop, value (repeating) — defaults to "equal_to"</item>
    /// </list>
    /// </para>
    /// </summary>
    public static List<FindCondition> Parse(object[] parameters)
    {
      var conditions = new List<FindCondition>();

      if (parameters.Length % 3 == 0)
      {
        for (var i = 0; i < parameters.Length; i += 3)
        {
          conditions.Add(new FindCondition
          {
            PropName = (string)parameters[i],
            FunctionName = parameters[i + 1].ToStringValue(),
            Value = parameters[i + 2]
          });
        }
      }
      else
      {
        for (var i = 0; i < parameters.Length; i += 2)
        {
          conditions.Add(new FindCondition
          {
            PropName = (string)parameters[i],
            FunctionName = "equal_to",
            Value = parameters[i + 1]
          });
        }
      }

      return conditions;
    }
  }
}
