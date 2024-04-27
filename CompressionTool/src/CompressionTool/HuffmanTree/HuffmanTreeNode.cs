
public class HuffmanTreeNode
{
  // An internal node has no element
  private char? element;
  private int weight;
  private HuffmanTreeNode? leftNode;
  private HuffmanTreeNode? rightNode;

  public HuffmanTreeNode(char? element, int weight)
  {
    this.element = element;
    this.weight = weight;
  }

  public char? GetElement()
  {
    return element;
  }

  public int GetWeight()
  {
    return weight;
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
}