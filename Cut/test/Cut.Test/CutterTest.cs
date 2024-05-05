
public class CutterTest
{
    [Theory]
    [FileDataAttribute("TestFiles/sample.tsv")]
    public void SimpleCutterTest(string[] content)
    {
      // Arrange
      int[] fields = {2};
      string[] expectedOutput = {
        "f1",
        "1",
        "6",
        "11",
        "16",
        "21",
      };

      // Act
      string[] result = Cutter.Cut(content, fields);

      // Assert
      Assert.Equal(expectedOutput.Length, result.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], result[i]);
      }
    }

    [Theory]
    [FileDataAttribute("TestFiles/fourchords.csv")]
    public void CommaDelimiterTest(string[] content)
    {
      // Arrange
      int[] fields = {1};
      string[] expectedOutput = {
        "Song title",
        "\"10000 Reasons (Bless the Lord)\"",
        "\"20 Good Reasons\"",
        "\"Adore You\"",
        "\"Africa\"",
      };

      // Act
      string[] result = Cutter.Cut(content, fields, ',');

      // Assert
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], result[i]);
      }
    }

     [Theory]
    [FileDataAttribute("TestFiles/sample.tsv")]
    public void MultiFieldTest(string[] content)
    {
      // Arrange
      int[] fields = {1, 2};
      string[] expectedOutput = {
        "f0\tf1",
        "0\t1",
        "5\t6",
        "10\t11",
        "15\t16",
        "20\t21",
      };

      // Act
      string[] result = Cutter.Cut(content, fields);

      // Assert
      Assert.Equal(expectedOutput.Length, result.Length);
      for(int i = 0; i < expectedOutput.Length; ++i)
      {
        Assert.Equal(expectedOutput[i], result[i]);
      }
    }
}