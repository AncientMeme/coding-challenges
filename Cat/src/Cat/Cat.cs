
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
      using (StreamReader reader = new(stream))
      {
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
          content.Add(line);
        }
      }
    }

    return content.ToArray();
  }

  public static string[] GetStandardInContent()
  {
    List<string> content = new();
    // Read StdIn
    using (Stream stream = Console.OpenStandardInput())
    using (StreamReader reader = new(stream))
    {
      string? line;
      while ((line = reader.ReadLine()) != null)
      {
        content.Add(line);
      }
    }

    return content.ToArray();
  }
}