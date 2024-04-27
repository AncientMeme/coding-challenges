
public class CharCounterTest
{
  [Fact]
  public void CharFrequencyTest()
  {
    // Arrange
    var counter = new CharCounter();
    var content = "dddaacccccbbb";

    // Act
    var dict = counter.GetCharFrequency(content);
    
    // Assert
    var expectedKeys = new char[]{'a', 'b', 'c', 'd'};
    foreach (var key in expectedKeys)
    {
      Assert.True(dict.ContainsKey(key));
    }
    var expectedValues = new int[]{2, 3, 5, 3};
    for (int i = 0; i < expectedKeys.Length; ++i)
    {
      Assert.Equal(expectedValues[i], dict[expectedKeys[i]]);
    }
  }
}