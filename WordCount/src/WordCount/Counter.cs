
using System.Text;
using System.Text.RegularExpressions;

public class Counter
{
  public static int GetBytes(FileStream stream)
  {
    int bytes = 0;
    while(stream.ReadByte() != -1)
    {
      bytes++;
    }
    return bytes;
  }

  public static int GetLines(string content)
  {
    int lines = Regex.Matches(content, "\n").Count;
    return lines;
  }

  public static int GetChars(string content)
  {
    // Includes EndOfFile character
    return content.Length + 1;
  }

  public static int GetWords(string content)
  {
    int words = Regex.Matches(content, @"\b\w+\b").Count;
    return words;
  }
}