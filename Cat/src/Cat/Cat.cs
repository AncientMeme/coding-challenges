
using System.Text;

public class Cat
{
  public static string[] GetFileContent(string[] files)
  {
    List<string> content = new();
    // Read files
    foreach (string file in files)
    {
      if (!File.Exists(file))
      {
        Console.WriteLine($"The file {file} does not exists");
        Environment.Exit(1);
      }

      using (FileStream stream = File.OpenRead(file))
      {
        content.AddRange(ReadStream(stream));
      }
    }
    return content.ToArray();
  }

  public static string[] GetStandardInContent()
  {
    List<string> content = new();
    // Read StdIn
    using (Stream stream = Console.OpenStandardInput())
    {
      content.AddRange(ReadStream(stream));
    }
    return content.ToArray();
  }

  public static string[] AddLineNumber(string[] lines, bool ignoreBlank)
  {
    List<string> numberedContent = new();
    int index = 0;
    foreach(string line in lines)
    {
      StringBuilder builder = new();
      // Does not add line number to blank lines if ignoreBlank is toggled
      if (!ignoreBlank || (ignoreBlank && !line.Equals("")))
      {
        builder.Append($"{index} ");
        index++;
      }
      
      builder.Append(line);
      numberedContent.Add(builder.ToString());
      
    }
    return numberedContent.ToArray();
  }

  private static string[] ReadStream(Stream stream)
  {
    List<string> lines = new();
    using (StreamReader reader = new(stream))
    {
      string? line;
      while ((line = reader.ReadLine()) != null)
      {
        lines.Add(line);
      }
    }
    return lines.ToArray();
  }
}