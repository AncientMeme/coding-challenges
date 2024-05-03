
public class NumParserTest
{
    [Fact]
    public void SimpleParserTest()
    {
      // Arrange
      var parser = new NumParser();
      var number = "eighty six";

      // Act
      long outcome = parser.GetNumber(number);

      // Assert
      Assert.Equal(86, outcome);
    }

    [Fact]
    public void MultiplierTest()
    {
      // Arrange
      var parser = new NumParser();
      var number = "a hundred thousand sixty nine";

      // Act
      long outcome = parser.GetNumber(number);

      // Assert
      Assert.Equal(100069, outcome);
    }

    [Fact]
    public void CompoundParserTest()
    {
      // Arrange
      var parser = new NumParser();
      var number = "three hundred million eighty thousand and eight";

      // Act
      long outcome = parser.GetNumber(number);

      // Assert
      Assert.Equal(300080008, outcome);
    }

    [Fact]
    public void ComplexNumberTest()
    {
       // Arrange
      var parser = new NumParser();
      var number = "five hundred thirty four thousand seven hundred and sixty six";

      // Act
      long outcome = parser.GetNumber(number);

      // Assert
      Assert.Equal(534766, outcome);
    }
}