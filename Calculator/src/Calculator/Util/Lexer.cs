
// Takes apart user input and construct tokens

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Util;

public enum TokenType
{
  Number,
  Operator,
  LeftBracket,
  RightBracket,
}
public struct Token : IEquatable<Token>
{
  public string value;
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
public class Lexer
{
  private readonly Dictionary<string, int> valueTable = new()
  {
    {"+", 2},
    {"-", 2},
    {"*", 3},
    {"/", 3},
  };
  private Queue<Token> tokens;
  private string input;
  private int index;
  public Lexer(string input)
  {
    this.tokens = new();
    this.input = input;
    this.index = 0;
  }

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
          string number = readNumber();
          tokens.Enqueue(new Token(number, TokenType.Number));
          break;
        case ' ':
          index++;
          break;
        default:
          throw new InvalidDataException($"Invalid character found: {c}");
      }
    }
    while(operators.Count > 0)
    {
      tokens.Enqueue(operators.Pop());
    }
    return tokens;
  }

  private string readNumber()
  {
    string validCharacters = "0123456789.";
    int startIndex = index;
    int length = 0;
    while(index < input.Length && validCharacters.Contains(input[index]))
    {
      length++;
      index++;
    }
    return input.Substring(startIndex, length);
  }

  private void ProcessOperators(Stack<Token> operators, Token token)
  {
    int precedent = valueTable[token.value];
    if (operators.Count == 0)
    {
      operators.Push(token);
    }
    else
    {
      // Maintain ascending precendent order in the stack
      int previousPrecedent = valueTable[operators.Peek().value];
      while (operators.Count > 0 && precedent < previousPrecedent)
      {
        Token previousToken = operators.Pop();
        tokens.Enqueue(previousToken);
        previousPrecedent = valueTable[operators.Peek().value];
      }
      operators.Push(token);
    }
  }
}

