
using System.Text;

public class Cutter
{
  public static string[] Cut(string[] entries, int[] targetFields, char delimiter='\t')
  {
    List<string> output = new();
    // Go through each line and grab the target fields
    foreach(string entry in entries)
    {
      StringBuilder builder = new();
      string[] fields = entry.Split(delimiter);
      
      for(int i = 0; i < fields.Length; ++i)
      {
        // Input is 1-indexed
        if (targetFields.Contains(i + 1))
        {
          builder.Append($"{fields[i]} ");
        }
      }
      builder.Remove(builder.Length - 1, 1);
      output.Add(builder.ToString());
    }

    return output.ToArray();
  }
}