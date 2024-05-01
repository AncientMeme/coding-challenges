using System.Text.RegularExpressions;

public class Counter
{
  public static int GetBytes(Stream stream)
  {
    return (int)stream.Length;
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
    int words = 0;
    bool inWord = false;
    char[] whitespace = {' ', '\t', '\r', '\n'};
    foreach(char c in content) {
      if (whitespace.Contains(c))
      {
        if (inWord)
        {
          inWord = false;
          words++;
        }
      }
      else
      {
        inWord = true;
      }
    }
    return words;
  }
}