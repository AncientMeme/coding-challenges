
public class HuffmanEncoder
{
  public static Dictionary<char, string> GetEncodingTable(HuffmanTreeNode rootNode)
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

  public static Dictionary<string, char> GetDecodingTable(HuffmanTreeNode rootNode)
  {
    Dictionary<string, char> decodingTable = new();
    // Flip the encoding table
    var encodingTable = GetEncodingTable(rootNode);
    foreach(KeyValuePair<char, string> entry in encodingTable)
    {
      decodingTable[entry.Value] = entry.Key;
    }

    return decodingTable;
  }
}