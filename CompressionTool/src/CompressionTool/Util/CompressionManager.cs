
using System.Text;

public class CompressionManager
{
  public CompressionManager()
  {

  }

  public void Compress(string content, string outputFileName)
  {
    // Get encode table
    var charFrequency = CharCounter.GetCharFrequency(content);
    var tree = HuffmanTreeBuilder.BuildTree(charFrequency);
    var encodeTable = HuffmanEncoder.GetEncodingTable(tree);

    // Pack content into bytes
    var header = GetHeaderInBytes(charFrequency);
    var body = HuffmanEncoder.Encode(content, encodeTable);
    
    // Write bytes to output file
    List<byte> output = new();
    output.AddRange(header);
    output.AddRange(body);
    using(FileStream fs = File.Open(outputFileName, FileMode.Create, FileAccess.Write))
    {
      fs.Write(output.ToArray());
    }
  }

  public void Decompress(FileStream stream, string outputFileName)
  {
    // Seperate header and body
    var charFrequency = GetCharFrequency(stream);
    string body = GetBody(stream);

    // Get Decoding Table
    var tree = HuffmanTreeBuilder.BuildTree(charFrequency);
    var encodingTable = HuffmanEncoder.GetEncodingTable(tree);
    var decodingTable = HuffmanEncoder.GetDecodingTable(tree);

    // Get True Length for body then decode
    int bodyLength = GetBodyLength(charFrequency, encodingTable);
    string content = DecodeBody(body, bodyLength, decodingTable);

    // Write content in output file
    using(FileStream file = File.Open(outputFileName, FileMode.Create, FileAccess.Write))
    using(StreamWriter writer = new(file))
    {
      writer.Write(content);
    }
  }

  private byte[] GetHeaderInBytes(Dictionary<char, int> frequency)
  {
    // Translate header
    StringBuilder sb = new();
    foreach (KeyValuePair<char, int> entry in frequency)
    {
      sb.Append($"{Convert.ToInt16(entry.Key)}:{entry.Value},");
    }
    sb.Append('|'); // Signals header end

    // Turn into bytes
    var headerBytes = sb.ToString().Select(c => (byte)c);
    List<byte> bytes = new();
    bytes.AddRange(headerBytes);
    return bytes.ToArray();
  }

  private Dictionary<char, int> GetCharFrequency(FileStream stream)
  {
    StringBuilder sb = new();
    bool gotSeperator = false;
    while (!gotSeperator)
    {
      int b = stream.ReadByte();
      // Reach end of file without seperator
      if (b == -1)
      {
        Console.WriteLine("This is not a Compressed File using Huffman Encoding");
        Environment.Exit(1);
      }

      char c = (char)b;
      if (c == '|')
      {
        gotSeperator = true;
      }
      else
      {
        sb.Append(c);
      }
    }

    // Convert Header into Frequency Table
    string[] rawData = sb.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
    var charFrequency = new Dictionary<char, int>();
    foreach(string entry in rawData)
    {
      string[] data = entry.Split(':');
      char key = Convert.ToChar(Convert.ToInt16(data[0], 10));
      int value = Convert.ToInt32(data[1]);
      charFrequency.Add(key, value);
    }
    return charFrequency;
  }

  private string GetBody(FileStream stream)
  {
    StringBuilder sb = new();
    bool streamEnd = false;
    while(!streamEnd)
    {
      int b = stream.ReadByte();
      if (b == -1)
      {
        streamEnd = true;
      }
      else
      {
        string byteString = Convert.ToString(b, 2).PadLeft(8, '0');
        sb.Append(byteString);
      }
    }

    return sb.ToString();
  }

  private int GetBodyLength(Dictionary<char, int> charFrequency, Dictionary<char, string> table)
  {
    int length = 0;
    foreach (KeyValuePair<char, int> entry in charFrequency)
    {
      int frequency = entry.Value;
      int codeLength = table[entry.Key].Length;
      length += frequency * codeLength;
    }
    return length;
  }

  private string DecodeBody(string body, int bits, Dictionary<string, char> table)
  {
    StringBuilder sb = new();
    string bitString = body[..bits];
    int index = 0;
    int size = 1;
    while(index + size <= bitString.Length)
    {
      string code = bitString.Substring(index, size);
      if (table.ContainsKey(code))
      {
        sb.Append(table[code]);
        index += size;
        size = 1;
      }
      else
      {
        size++;
      }
    }

    return sb.ToString();
  }
}