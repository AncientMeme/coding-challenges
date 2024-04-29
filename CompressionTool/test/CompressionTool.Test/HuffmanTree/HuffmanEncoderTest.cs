
public class HuffmanEncoderTest
{
  [Fact]
  public void GetCodeFromTreeTest()
  {
    // Arrange
    var RootNode = new HuffmanTreeNode(null, 9);
    var node1_0 = new HuffmanTreeNode(null, 4);
    var node1_1 = new HuffmanTreeNode('c', 5);
    var node2_0 = new HuffmanTreeNode('b', 1);
    var node2_1 = new HuffmanTreeNode('a', 3);
    RootNode.SetLeftNode(node1_0);
    RootNode.SetRightNode(node1_1);
    node1_0.SetLeftNode(node2_0);
    node1_0.SetRightNode(node2_1);
    var expectedTable = new Dictionary<char, string>()
    {
      {'a', "01"},
      {'b', "00"},
      {'c', "1"}
    };
    
    // Act
    var table = HuffmanEncoder.GetEncodingTable(RootNode);

    // Assert
    foreach(KeyValuePair<char, string> entry in expectedTable)
    {
      Assert.True(table.ContainsKey(entry.Key));
      Assert.Equal(entry.Value, table[entry.Key]);
    }
  }

  [Fact]
  public void EncodeTest()
  {
    // Arrange
    var table = new Dictionary<char, string>()
    {
      {'a',  "0"},
      {'\n', "10"},
      {'\r', "110"},
      {'b',  "111"},
    };
    string content = "aa\r\nb";
    byte[] expectedBytes = {53, 192};

    // Act
    var encodeBytes = HuffmanEncoder.Encode(content, table);
    
    // Assert
    Assert.Equal(expectedBytes.Length, encodeBytes.Length);
    for(int i = 0; i < expectedBytes.Length; ++i)
    {
      Assert.Equal(expectedBytes[i], encodeBytes[i]);
    }
  }

  [Fact]
  public void DecodeTest()
  {
    // Arrange
    var table = new Dictionary<string, char>()
    {
      {"0", 'a'},
      {"10", '\n'},
      {"110", '\r'},
      {"111",  'b'},
    };
    byte[] bytes = {53, 192};
    string ExpectedString = "aa\r\nb";

    // Act
    var decodedContent = HuffmanEncoder.Decode(bytes, 10, table);
    
    // Assert
    Assert.Equal(ExpectedString, decodedContent); 
  }
}