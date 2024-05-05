// Handle the busy work of reading files and converting input to useful format
public class InputParser
{
  public static string[]? GetEntries(string? file)
  {
    List<string> entries = new();
    Stream stream;
    StreamReader reader;
    if (file == null || file.Equals("-"))
    {
      // Read from stdin
      stream = Console.OpenStandardInput();
      reader = new(stream);
    }
    else
    {
      // Read from file
      stream = File.OpenRead(file);
      reader = new(stream);
    }
    
    string? line;
    while((line = reader.ReadLine()) != null)
    {
      entries.Add(line);
    }
    reader.Close();

    return entries.ToArray();
  }

  public static int[] GetFields(string fields)
  {
    List<int> selectedFields = new();
    char[] seperators = {' ', ','};
    string[] fieldEntries = fields.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
    foreach (string entry in fieldEntries)
    {
      selectedFields.Add(Convert.ToInt32(entry));
    }
    return selectedFields.ToArray();
  }
}