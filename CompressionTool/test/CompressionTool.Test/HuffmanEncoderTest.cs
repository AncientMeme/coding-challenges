
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
    var encoder = new HuffmanEncoder();

    // Act
    var table = encoder.GetCodeFromTree(RootNode);

    // Assert
    foreach(KeyValuePair<char, string> entry in expectedTable)
    {
      Assert.True(table.ContainsKey(entry.Key));
      Assert.Equal(entry.Value, table[entry.Key]);
    }
  }
}