
public class HuffmanTreeBuilderTest
{
  // Builder should handle empty string without error
  [Fact]
  public void BuildEmptyTree()
  {
    // Arrange
    var builder = new HuffmanTreeBuilder();
    var frequency = new Dictionary<char, int>();
    var expectedNode = new HuffmanTreeNode(null, 0);

    // Act
    var rootNode = builder.BuildTree(frequency);

    // Assert
    Assert.True(rootNode.Equals(expectedNode));
  }

  // Builder should be able to build a tree from frequency table
  [Fact]
  public void BuildBasicTreeTest()
  {
    // Arrange
    // Expected Tree
    var expectedRootNode = new HuffmanTreeNode(null, 9);
    var node1_0 = new HuffmanTreeNode(null, 4);
    var node1_1 = new HuffmanTreeNode('c', 5);
    var node2_0 = new HuffmanTreeNode('b', 1);
    var node2_1 = new HuffmanTreeNode('a', 3);
    expectedRootNode.SetLeftNode(node1_0);
    expectedRootNode.SetRightNode(node1_1);
    node1_0.SetLeftNode(node2_0);
    node1_0.SetRightNode(node2_1);
    
    var builder = new HuffmanTreeBuilder();
    var frequency = new Dictionary<char, int>(){
      {'a', 3},
      {'b', 1},
      {'c', 5},
    };

    // Act
    HuffmanTreeNode rootNode = builder.BuildTree(frequency);

    // Assert
    Assert.True(IsIdenticalTree(expectedRootNode, rootNode));
  }

  private bool IsIdenticalTree(HuffmanTreeNode? nodeA, HuffmanTreeNode? nodeB)
  {
    // Both empty
    if (nodeA == null && nodeB == null)
    {
      return true;
    }

    // Both not empty
    if (nodeA != null && nodeB != null)
    {
      return nodeA.Equals(nodeB) 
        && IsIdenticalTree(nodeA.GetLeftNode(), nodeB.GetLeftNode())
        && IsIdenticalTree(nodeA.GetRightNode(), nodeB.GetRightNode());
    }

    // Only one side empty, not identical
    return false;
  }
}