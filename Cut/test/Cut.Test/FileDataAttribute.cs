
using System.Reflection;
using Xunit.Sdk;

public class FileDataAttribute : DataAttribute
{
  private readonly string _filePath;
  public FileDataAttribute(string filePath)
  {
    _filePath = filePath;
  }

  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    // Read content from file
    List<string> entries = new();
    using(FileStream file = File.OpenRead(_filePath))
    using(StreamReader reader = new(file))
    {
      string? line;
      while ((line = reader.ReadLine()) != null)
      {
        entries.Add(line);
      }
    }
    List<string[][]> output = new();
    string[][] container = {entries.ToArray()};
    output.Add(container);

    return output;
  }
}