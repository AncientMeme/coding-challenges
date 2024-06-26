
public class CharCounter
{
  public static Dictionary<char, int> GetCharFrequency(string content)
  {
    Dictionary<char, int> dict = new();
    foreach(char c in content)
    {
      if(!dict.ContainsKey(c))
      {
        dict[c] = 0;
      }
      dict[c]++;
    }

    return dict;
  }
}