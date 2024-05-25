using Util;
namespace Calculator.Test;

public class LexerTest
{
    [Fact]
    public void GetTokens_SingleOperaton_ReturnTokens()
    {
      // Arrange
      string input = "2 + 4";
      List<Token> expectedTokens = new()
      {
        new Token("2", TokenType.Number),
        new Token("4", TokenType.Number),
        new Token("+", TokenType.Operator),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_Operatons_ReturnTokens()
    {
      // Arrange
      string input = "3 + 16 / 2";
      List<Token> expectedTokens = new()
      {
        new Token("3", TokenType.Number),
        new Token("16", TokenType.Number),
        new Token("2", TokenType.Number),
        new Token("/", TokenType.Operator),
        new Token("+", TokenType.Operator),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_Floats_ReturnTokens()
    {
      // Arrange
      string input = "4.5 * 11 - 2";
      List<Token> expectedTokens = new()
      {
        new Token("4.5", TokenType.Number),
        new Token("11", TokenType.Number),
        new Token("*", TokenType.Operator),
        new Token("2", TokenType.Number),
        new Token("-", TokenType.Operator),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_Brackets_ReturnTokens()
    {
      string input = "(2 + 4) - 3";
      List<Token> expectedTokens = new()
      {
        new Token("2", TokenType.Number),
        new Token("4", TokenType.Number),
        new Token("+", TokenType.Operator),
        new Token("3", TokenType.Number),
        new Token("-", TokenType.Operator),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_ComplexBracket_ReturnTokens()
    {
      string input = "(2 + 4 / 2) / 2";
      List<Token> expectedTokens = new()
      {
        new Token("2", TokenType.Number),
        new Token("4", TokenType.Number),
        new Token("2", TokenType.Number),
        new Token("/", TokenType.Operator),
        new Token("+", TokenType.Operator),
        new Token("2", TokenType.Number),
        new Token("/", TokenType.Operator),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_Sine_ReturnTokens()
    {
      // Arrange
      string input = "sin(2)";
      List<Token> expectedTokens = new()
      {
        new Token("2", TokenType.Number),
        new Token("sin", TokenType.Function),
      };
      // Act
      Lexer lexer = new(input);
      Queue<Token> tokens = lexer.GetTokens();
      // Assert
      Assert.Equal(expectedTokens.Count, tokens.Count);
      foreach(Token expectedToken in expectedTokens)
      {
        Token token = tokens.Dequeue();
        Assert.Equal(expectedToken.value, token.value);
        Assert.Equal(expectedToken.type, token.type);
      }
    }

    [Fact]
    public void GetTokens_BadInput_ThrowException()
    {
      // Arrange
      string input = "3 + c - 12";
      // Act
      Lexer lexer = new(input);
      Action act = () => lexer.GetTokens();
      // Assert
      InvalidDataException e = Assert.Throws<InvalidDataException>(act);
      Assert.Equal("Invalid expression found: c", e.Message);
    }

    [Fact]
    public void GetTokens_BadInput2_ThrowException()
    {

    }
}