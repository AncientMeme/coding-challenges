
using System.Reflection;
using Xunit.Sdk;

public class TextfileDataAttribute: DataAttribute
{
  private readonly string _filePath;

  public TextfileDataAttribute(string filePath)
  {
    _filePath = filePath;
  }

  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    var path = Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);
    if (!File.Exists(path))
    {
      throw new FileNotFoundException("Could not find file at: {path}");
    }

    string content;
    using(FileStream file = File.OpenRead(path))
    using(StreamReader reader = new(file))
    {
      content = reader.ReadToEnd();
    }

    FileInfo info = new(path);
    object[] entries = {
      info,
      content
    };
    List<object[]> data = new(){entries};

    return data;
  }
}