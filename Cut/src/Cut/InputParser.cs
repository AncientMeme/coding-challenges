// Handle the busy work of reading files and converting input to useful format
public class InputParser
{
  public static string[]? GetEntries(string? file)
  {
    List<string> entries = new();
    if (file == null || file.Equals("-"))
    {
      return null;
    }
    else
    {
      using(FileStream stream = File.OpenRead(file))
      using(StreamReader reader = new(stream))
      {
        string? line;
        while((line = reader.ReadLine()) != null)
        {
          entries.Add(line);
        }
      }
    }

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