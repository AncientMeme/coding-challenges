
public class CatTest
{
    [Fact]
    public void BasicCatTest()
    {
      // Arrange
      string[] files = {"TestFiles/text1.txt"};
      string[] expectedOutput = {
        "\"Life isn’t about getting and having, it’s about giving and being.\"",
        "\"Whatever the mind of man can conceive and believe, it can achieve.\"",
        "\"Strive not to be a success, but rather to be of value.\""
      };

      // Act
      string[] output = Cat.GetFileContent(files);

      // Assert
      Assert.Equal(expectedOutput.Length, output.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], output[i]);
      }
    }

    [Fact]
    public void MultipleFileTest()
    {
      // Arrange
      string[] files = {"TestFiles/text1.txt", "TestFiles/text2.txt"};
      string[] expectedOutput = {
        "\"Life isn’t about getting and having, it’s about giving and being.\"",
        "\"Whatever the mind of man can conceive and believe, it can achieve.\"",
        "\"Strive not to be a success, but rather to be of value.\"",
        "\"Two roads diverged in a wood, and I—I took the one less traveled by, And that has made all the difference.\"",
        "\"I attribute my success to this: I never gave or took any excuse.\"",
        "\"You miss 100% of the shots you don’t take.\"",
      };

      // Act
      string[] output = Cat.GetFileContent(files);

      // Assert
      Assert.Equal(expectedOutput.Length, output.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], output[i]);
      }
    }

    [Fact]
    public void NumberLineTest()
    {
      // Arrange
      string[] files = {"TestFiles/text1.txt", "TestFiles/text2.txt"};
      string[] expectedOutput = {
        "0 \"Life isn’t about getting and having, it’s about giving and being.\"",
        "1 \"Whatever the mind of man can conceive and believe, it can achieve.\"",
        "2 \"Strive not to be a success, but rather to be of value.\"",
        "3 \"Two roads diverged in a wood, and I—I took the one less traveled by, And that has made all the difference.\"",
        "4 \"I attribute my success to this: I never gave or took any excuse.\"",
        "5 \"You miss 100% of the shots you don’t take.\"",
      };

      // Act
      string[] output = Cat.GetFileContent(files);
      output = Cat.AddLineNumber(output, false);

      // Assert
      Assert.Equal(expectedOutput.Length, output.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], output[i]);
      }
    }

    [Fact]
    public void BlankLineTest()
    {
      // Arrange
      string[] files = {"TestFiles/blank.txt"};
      string[] expectedOutput = {
        "0 \"Life isn’t about getting and having, it’s about giving and being.\"",
        "1 ",
        "2 \"Whatever the mind of man can conceive and believe, it can achieve.\"",
        "3 ",
        "4 \"Strive not to be a success, but rather to be of value.\""
      };

      // Act
      string[] output = Cat.GetFileContent(files);
      output = Cat.AddLineNumber(output, false);

      // Assert
      Assert.Equal(expectedOutput.Length, output.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], output[i]);
      }
    }

    [Fact]
    public void NonBlankLineTest()
    {
      // Arrange
      string[] files = {"TestFiles/blank.txt"};
      string[] expectedOutput = {
        "0 \"Life isn’t about getting and having, it’s about giving and being.\"",
        "",
        "1 \"Whatever the mind of man can conceive and believe, it can achieve.\"",
        "",
        "2 \"Strive not to be a success, but rather to be of value.\""
      };

      // Act
      string[] output = Cat.GetFileContent(files);
      output = Cat.AddLineNumber(output, true);

      // Assert
      Assert.Equal(expectedOutput.Length, output.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], output[i]);
      }
    }
}