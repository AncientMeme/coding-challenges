
using System.Text;

public class CompressionManager
{
  public void CompressFile(string content, string outputFileName)
  {
    // Generate Header
    var charFrequency = CharCounter.GetCharFrequency(content);
    string header = GenerateEncodingHeader(charFrequency);

    // Generate Encoded Content
    var treeBuilder = new HuffmanTreeBuilder();
    var HuffmanTree = treeBuilder.BuildTree(charFrequency);
    var encoder = new HuffmanEncoder();
    var encodeTable = encoder.GetEncodingTable(HuffmanTree);
    byte[] body = GenerateEncodedContent(content, encodeTable);

    using(FileStream outputFile = File.Open(outputFileName, FileMode.Create, FileAccess.Write))
    using(StreamWriter writer = new(outputFile))
    {
      writer.Write(header);
      writer.Flush();
      outputFile.Write(body);
    }
  }

  private string GenerateEncodingHeader(Dictionary<char, int> charFrequency)
  {
    StringBuilder header = new();
    foreach(KeyValuePair<char, int> entry in charFrequency)
    {
      string section = $"{entry.Key}:{entry.Value},";
      header.Append(section);
    }
    // replace final comma with line break
    header.Remove(header.Length - 1, 1);
    header.Append('\n');

    return header.ToString();
  }

  private byte[] GenerateEncodedContent(string content, Dictionary<char, string> encodeTable)
  {
    StringBuilder body = new();
    // Convert char to binary strings
    foreach (char c in content)
    {
      body.Append(encodeTable[c]);
    }
    // Ensure the content length is multiple of 8 (one byte)
    while(body.Length % 8 != 0)
    {
      body.Append('0');
    }
    string binaryString = body.ToString();
    
    // Convert binary strings to chunks of bytes
    var bytes = new List<byte>();
    int index = 0;
    while (index < binaryString.Length)
    {
      string chunk = binaryString.Substring(index, 8);
      int chunkValue = Convert.ToInt32(chunk, 2);
      bytes.Add((byte)chunkValue);
      index += 8;
    }

    return bytes.ToArray();
  }
}