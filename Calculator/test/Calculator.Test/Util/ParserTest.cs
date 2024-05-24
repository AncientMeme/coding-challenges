using Util;
namespace Calculator.Test;

public class ParserTest
{
  [Fact]
  public void GetAnswer_SimpleExpression_ReturnCorrectAnswer()
  {
    // Arrange
    Queue<Token> tokens = new();
    tokens.Enqueue(new Token("3", TokenType.Number));
    tokens.Enqueue(new Token("7", TokenType.Number));
    tokens.Enqueue(new Token("+", TokenType.Operator));

    // Act
    Parser parser = new(tokens);
    var answer = parser.GetAnswer();
    // Assert
    Assert.Equal(10.0, answer);
  }

  [Fact]
  public void GetAnswer_ComplexExpression_ReturnCorrectAnswer()
  {
    // Arrange
    Queue<Token> tokens = new(); // (3 + 7) / 10 + 3
    tokens.Enqueue(new Token("3", TokenType.Number));
    tokens.Enqueue(new Token("7", TokenType.Number));
    tokens.Enqueue(new Token("+", TokenType.Operator));
    tokens.Enqueue(new Token("10", TokenType.Number));
    tokens.Enqueue(new Token("/", TokenType.Operator));
    tokens.Enqueue(new Token("3", TokenType.Number));
    tokens.Enqueue(new Token("+", TokenType.Operator));

    // Act
    Parser parser = new(tokens);
    var answer = parser.GetAnswer();
    // Assert
    Assert.Equal(4.0, answer);
  }

  [Fact]
  public void GetAnswer_Floats_ReturnCorrectAnswer()
  {
    // Arrange
    Queue<Token> tokens = new(); // (3 + 7) / 10 + 3
    tokens.Enqueue(new Token("3.5", TokenType.Number));
    tokens.Enqueue(new Token("3.5", TokenType.Number));
    tokens.Enqueue(new Token("+", TokenType.Operator));
    tokens.Enqueue(new Token("2.4", TokenType.Number));
    tokens.Enqueue(new Token("-", TokenType.Operator));

    // Act
    Parser parser = new(tokens);
    var answer = parser.GetAnswer();
    // Assert
    Assert.Equal(4.6, answer);
  }
}