
public class HuffmanEncoder
{
  public Dictionary<char, string> GetCodeFromTree(HuffmanTreeNode rootNode)
  {
    var codeTable = new Dictionary<char, string>();
    var queue = new Queue<(HuffmanTreeNode, string)>();
    queue.Enqueue((rootNode, ""));
    while (queue.Count > 0)
    {
      var (node, digits) = queue.Dequeue();
      // Traverse through nodes
      var leftNode = node.GetLeftNode();
      if (leftNode != null)
      {
        queue.Enqueue((leftNode, digits + "0"));
      }
      var rightNode = node.GetRightNode();
      if (rightNode != null)
      {
        queue.Enqueue((rightNode, digits + "1"));
      }

      // Node is a leaf, add to table
      if (node.isLeaf() && node.element != null)
      {
        codeTable[(char)node.element] = digits; 
      }
    }

    return codeTable; 
  }
}