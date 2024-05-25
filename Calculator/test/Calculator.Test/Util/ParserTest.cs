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

  [Fact]
  public void GetAnswer_Sine_ReturnCorrectAnswer()
  {
    // Arrange
    Queue<Token> tokens = new(); // sin(90) = 0.894
    tokens.Enqueue(new Token("90", TokenType.Number));
    tokens.Enqueue(new Token("sin", TokenType.Function));

    // Act
    Parser parser = new(tokens);
    var answer = parser.GetAnswer();
    // Assert
    Assert.Equal(0.894, answer);
  }

  [Fact]
  public void GetAnswer_Trigonometry_ReturnCorrectAnswer()
  {
    // Arrange
    Queue<Token> tokens = new(); // cos(90) + tan(180) = -1
    tokens.Enqueue(new Token("90", TokenType.Number));
    tokens.Enqueue(new Token("cos", TokenType.Function));
    tokens.Enqueue(new Token("180", TokenType.Number));
    tokens.Enqueue(new Token("tan", TokenType.Function));
    tokens.Enqueue(new Token("+", TokenType.Operator));

    // Act
    Parser parser = new(tokens);
    var answer = parser.GetAnswer();
    // Assert
    Assert.Equal(0.891, answer);
  }

  [Fact]
  public void GetAnswer_DivideByZero_ThrowException()
  {
    // Arrange
    Queue<Token> tokens = new(); // (1/ 0)
    tokens.Enqueue(new Token("1", TokenType.Number));
    tokens.Enqueue(new Token("0", TokenType.Number));
    tokens.Enqueue(new Token("/", TokenType.Operator));

    // Act
    Parser parser = new(tokens);
    Action act = () => parser.GetAnswer();
    // Assert
    DivideByZeroException e = Assert.Throws<DivideByZeroException>(act);
    Assert.Equal("Expression resulted in division by zero", e.Message);
  }
}