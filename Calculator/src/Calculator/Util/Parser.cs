
namespace Util;
/// <summary>
/// A parser for consuming a token queue. The queue will be empty after value calculation
/// </summary>

public class Parser
{
  /// <summary>
  /// The table containing all the functions with <c>Token.Value</c> as key.
  /// </summary>
  private Dictionary<string, Func<double, double, double>> operationTable = new()
  {
    {"+", (a, b) => a + b},
    {"-", (a, b) => a - b},
    {"*", (a, b) => a * b},
    {"/", (a, b) => a / b},
    {"sin", (a, b) => Math.Sin(a)},
    {"cos", (a, b) => Math.Cos(a)},
    {"tan", (a, b) => Math.Tan(a)},
  };
  /// <value>
  /// The <c>Token</c> queue to consume in order to generate the outcome
  /// </value>
  private Queue<Token> tokens;
  
  public Parser(Queue<Token> tokens)
  {
    this.tokens = tokens;
  }

  /// <summary>
  /// Consume the <c>Token</c> queue and calculate the answer.
  /// </summary>
  /// <returns>The answer to the math expression.</returns>
  /// <exception cref="InvalidOperationException">
  /// Thrown when the Token Queue is Invalid
  /// </exception>
  /// <exception cref="InvalidDataException">
  /// Thrown when there are Invalid Token
  /// </exception>
  public double GetAnswer()
  {
    Stack<double> numbers = new();
    while(tokens.Count > 0)
    {
      Token token = tokens.Dequeue();
      switch (token.type)
      {
        case TokenType.Number:
          numbers.Push(Convert.ToSingle(token.value));
          break;
        case TokenType.Operator:
          if (numbers.Count < 2)
          {
            throw new InvalidOperationException("Invalid math operation was passed in");
          }
          double right = numbers.Pop();
          double left = numbers.Pop();
          double OperatorResult = Operate(left, right, token);
          numbers.Push(OperatorResult);
          break;
        case TokenType.Function:
          if (numbers.Count < 1)
          {
            throw new InvalidOperationException("Invalid math function was passed in");
          }
          double number = numbers.Pop();
          double functionResult = ProcessFunction(number, token);
          numbers.Push(functionResult);
          break;
        default:
          throw new InvalidDataException($"Token queue contains invalid Token: {token.value}");
      }
    }
    // 3 digit accuracy
    double output = numbers.Pop();
    output = Math.Round(output * 1000) / 1000;
    return output;
  }

  /// <summary>
  /// Perform the operation and return the value
  /// </summary>
  /// <param name="left">The number on the left of operation</param>
  /// <param name="right">The number on the right of operation<</param>
  /// <param name="token">The operation Token</param>
  /// <returns></returns>
  private double Operate(double left, double right, Token token)
  {
    // Check for invalid tokens
    if (!operationTable.ContainsKey(token.value))
    {
      throw new InvalidOperationException($"Unknown operation: {token.value}");
    }
    if (token.value.Equals("/") && right == 0.0)
    {
      throw new DivideByZeroException("Expression resulted in division by zero");
    }
    return operationTable[token.value](left, right);
  }

  /// <summary>
  /// Runs the math function according to the token value
  /// </summary>
  /// <param name="number">The number to apply the function to</param>
  /// <param name="token">The token containing the function</param>
  /// <returns>The result of the function</returns>
  /// <exception cref="InvalidOperationException">
  /// The token contains unknown function
  /// </exception>
  private double ProcessFunction(double number, Token token)
  {
    // Check for invalid tokens
    if (!operationTable.ContainsKey(token.value))
    {
      throw new InvalidOperationException($"Unknown operation: {token.value}");
    }

    return operationTable[token.value](number, number);
  }
}