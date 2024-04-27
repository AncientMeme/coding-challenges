
using System.Text;

public class CompressionManager
{
  public void CompressFile(string content, string outputFileName)
  {
    // Generate Header
    var charFrequency = CharCounter.GetCharFrequency(content);
    string header = GenerateEncodingHeader(charFrequency);

    using(FileStream outputFile = File.Open(outputFileName, FileMode.Create, FileAccess.Write))
    using(StreamWriter writer = new(outputFile))
    {
      writer.Write(header);
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
}