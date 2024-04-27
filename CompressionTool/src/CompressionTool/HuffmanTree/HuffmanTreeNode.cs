
public class HuffmanTreeNode
{
  // An internal node has no element
  public char? element;
  public int weight;
  private HuffmanTreeNode? leftNode;
  private HuffmanTreeNode? rightNode;

  public HuffmanTreeNode(char? element, int weight)
  {
    this.element = element;
    this.weight = weight;
  }

  public HuffmanTreeNode? GetLeftNode()
  {
    return leftNode;
  }

  public HuffmanTreeNode? GetRightNode()
  {
    return rightNode;
  }

  public void SetLeftNode(HuffmanTreeNode node)
  {
    leftNode = node;
  }

  public void SetRightNode(HuffmanTreeNode node)
  {
    rightNode = node;
  }

  public bool isLeaf()
  {
    return leftNode == null && rightNode == null;
  }

  public bool Equals(HuffmanTreeNode node)
  {
    return node.element == element && node.weight == weight;
  }
}