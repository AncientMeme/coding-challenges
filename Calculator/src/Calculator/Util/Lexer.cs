
namespace Util;

/// <summary>
/// Enum <c>TokenType</c> Value to identify the type of Token
/// </summary>
public enum TokenType
{
  Number,
  Operator,
  Function,
  LeftBracket,
  RightBracket,
}
/// <summary>
/// Token that represents a number or operation.
/// </summary>
public struct Token : IEquatable<Token>
{
  /// <value>
  /// The value of the token, could be an number or operation
  /// </value>
  public string value;
  /// <value>
  /// The type of token this is
  /// <see cref="TokenType"/>
  /// </value>
  public TokenType type;

  public Token(string value, TokenType type)
  {
    this.value = value;
    this.type = type;
  }

  public override bool Equals(object? obj) => obj is Token && this.Equals(obj);

  public bool Equals(Token token)
  {
    return value.Equals(token.value) && type == token.type;
  }

  public override int GetHashCode()
  {
    return (type, value).GetHashCode();
  }
}
///<summary>
/// Class <c>Lexer</c> Takes apart user input and construct tokens
///</summary>
public class Lexer
{
  /// <value>
  /// The table to lookup precedent for the operation
  /// </value>
  private readonly Dictionary<string, int> precedentTable = new()
  {
    {"+", 2},
    {"-", 2},
    {"*", 3},
    {"/", 3},
    {"^", 4},
  };
  private readonly HashSet<string> functionTable = new()
  {
    {"sin"},
    {"cos"},
    {"tan"},
  };
  /// <value>
  /// The token queue to enqueue all generated token
  /// </value>
  private Queue<Token> tokens;
  /// <value>
  /// The math expression in string form from the user
  /// </value>
  private string input;
  /// <value>
  /// The index pointing at the character in <c>input</c>
  /// </value>
  private int index;
  public Lexer(string input)
  {
    this.tokens = new();
    this.input = input;
    this.index = 0;
  }

  /// <summary>
  /// Returns a new <c>Queue</c> for parser to read. <see cref="Parser"/>
  /// </summary>
  /// <returns>A <c>Queue</c> containing <c>Token</c> in Reverse Polish Notation.</returns>
  /// <exception cref="InvalidDataException">
  /// When the input contains a character that is not a number or operation
  /// </exception>
  public Queue<Token> GetTokens()
  {
    tokens = new();
    index = 0;
    Stack<Token> operators = new(); // monotonic increase stack
    char c;
    while(index < input.Length)
    {
      c = input[index];
      switch(c)
      {
        case '+' or '-' or '*' or '/':
          Token token = new($"{c}", TokenType.Operator);
          ProcessOperators(operators, token);
          index++;
          break;
        case '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
          ProcessNumber();
          break;
        case ' ':
          index++;
          break;
        case '(':
          operators.Push(new Token("(", TokenType.LeftBracket));
          index++;
          break;
        case ')':
          ProcessBracket(operators);
          index++;
          break;
        default:
          ProcessFunction(operators);
          break;
      }
    }
    while(operators.Count > 0)
    {
      tokens.Enqueue(operators.Pop());
    }
    return tokens;
  }

  /// <summary>
  /// Read the number in <c>input</c> and insert it into the queue as a token
  /// </summary>
  private void ProcessNumber()
  {
    string validCharacters = "0123456789.";
    int startIndex = index;
    int length = 0;
    while(index < input.Length && validCharacters.Contains(input[index]))
    {
      length++;
      index++;
    }
    string number = input.Substring(startIndex, length);
    tokens.Enqueue(new Token(number, TokenType.Number));
  }

  /// <summary>
  /// Process a token by pushing into the stack and enqueue the previous tokens
  /// </summary>
  /// <param name="operators">The reference to the operator stack</param>
  /// <param name="token">The token to be inserted</param>
  private void ProcessOperators(Stack<Token> operators, Token token)
  {
    int precedent = precedentTable[token.value];
    if (operators.Count == 0)
    {
      operators.Push(token);
    }
    else
    {
      // Maintain ascending precendent order in the stack
      Token lastToken = operators.Peek();
      while (operators.Count > 0 && lastToken.type != TokenType.LeftBracket && precedent < precedentTable[lastToken.value])
      {
        Token previousToken = operators.Pop();
        tokens.Enqueue(previousToken);
        if (operators.Count > 0)
        {
          lastToken = operators.Peek();
        }
      }
      operators.Push(token);
    }
  }

  /// <summary>
  /// Push out all previous operators until left bracket is reached
  /// </summary>
  /// <param name="operators">The operator token stack</param>
  private void ProcessBracket(Stack<Token> operators)
  {
    // Pop tokens until encountering left bracket
    while(operators.Count > 0 && operators.Peek().type != TokenType.LeftBracket)
    {
      tokens.Enqueue(operators.Pop());
    }
    if (operators.Count == 0)
    {
      throw new InvalidDataException("Could not find left bracket in expression");
    }

    // Discard left bracket
    operators.Pop();

    if (operators.Count > 0 && operators.Peek().type == TokenType.Function)
    {
      tokens.Enqueue(operators.Pop());
    }
  }

  private void ProcessFunction(Stack<Token> operators)
  {
    // Scan the function
    int startIndex = index;
    int length = 0;
    while(index < input.Length && Char.IsLetter(input[index]))
    {
      length++;
      index++;
    }

    // Check if the function exists
    string function = input.Substring(startIndex, length);
    if (functionTable.Contains(function))
    {
      operators.Push(new Token(function, TokenType.Function));
    }
    else
    {
      throw new InvalidDataException($"Invalid expression found: {function}");
    }
  }
}

