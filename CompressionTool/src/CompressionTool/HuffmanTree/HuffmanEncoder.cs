
using System.Text;

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

  public static byte[] Encode(string content, Dictionary<char, string> encodingTable)
  {
    List<byte> bytes = new();
    StringBuilder builder = new();
    foreach(char c in content)
    {
      builder.Append(encodingTable[c]);
    }

    // Pad 0s on right until length is multiple of 8 (byte)
    while(builder.Length % 8 != 0)
    {
      builder.Append('0');
    }
    string binaryString = builder.ToString();

    // Transform binary string to bytes
    int index = 0;
    while (index < binaryString.Length)
    {
      string chunk = binaryString.Substring(index, 8);
      bytes.Add(Convert.ToByte(chunk, 2));
      index += 8;
    }

    return bytes.ToArray();
  }

  public static string Decode(byte[] bytes, int bits, Dictionary<string, char> decodingTable)
  {
    // Bytes -> BinaryString
    StringBuilder binary = new();
    foreach(byte b in bytes)
    {
      binary.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
    }
    string binaryString = binary.ToString()[..bits];

    // BinaryString -> String
    StringBuilder builder = new();
    int index = 0;
    int size = 1;
    while(index + size <= binaryString.Length)
    {
      var chunk = binaryString.Substring(index, size);
      if (decodingTable.ContainsKey(chunk))
      {
        builder.Append(decodingTable[chunk]);
        index += size;
        size = 0;
      }
      else
      {
        size++;
      }
    }

    return builder.ToString();
  }
}