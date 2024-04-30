namespace WordCount.Test;

public class CounterTest
{
    [Theory]
    [TextfileData("test.txt")]
    public void GetByteTest(FileInfo fileInfo, string content)
    {
      // Arrange
      var _ = content;
      FileStream stream = fileInfo.OpenRead();

      // Act
      int byteCount = Counter.GetBytes(stream);
      stream.Close();

      // Assert
      Assert.Equal(342190, byteCount);
    }

    [Theory]
    [TextfileData("test.txt")]
    public void GetLineTest(FileInfo fileInfo, string content)
    {
      // Arrange
      var _ = fileInfo;

      // Act
      int lineCount = Counter.GetLines(content);

      // Assert
      Assert.Equal(7145, lineCount);
    }

    [Theory]
    [TextfileData("test.txt")]
    public void GetWordsTest(FileInfo fileInfo, string content)
    {
      // Arrange
      var _ = fileInfo;

      // Act
      int wordCount = Counter.GetWords(content);

      // Assert
      Assert.Equal(58164, wordCount);
    }

    [Theory]
    [TextfileData("test.txt")]
    public void GetCharTest(FileInfo fileInfo, string content)
    {
      // Arrange
      var _ = fileInfo;

      // Act
      int charCount = Counter.GetChars(content);

      // Assert
      Assert.Equal(339292, charCount);
    }
}