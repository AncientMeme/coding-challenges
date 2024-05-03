
public class NumParser
{
  private readonly Dictionary<string, long> numberTable = new()
  {
    {"a",        1},
    {"one",      1},
    {"two",      2},
    {"three",    3},
    {"four",     4},
    {"five",     5},
    {"six",      6},
    {"seven",    7},
    {"eight",    8},
    {"nine",     9},
    {"ten",     10},
    {"twenty",  20},
    {"thirty",  30},
    {"forty",   40},
    {"fifty",   50},
    {"sixty",   60},
    {"seventy", 70},
    {"eighty",  80},
    {"ninty",   90},
  };
  private readonly Dictionary<string, long> multiplierTable = new()
  {
    {"and",      1},
    {"hundred",  100},
    {"thousand", 1000},
    {"million",  1000000},
    {"billion",  1000000000},
    {"trillion", 1000000000000}
  };

  public long GetNumber(string number)
  {
    var tokens = GetTokens(number);
    long output = 0;
    long currentSum = 0;
    bool inSperator = false;
    foreach(NumToken token in tokens)
    {
      if (token.type == TokenType.Number)
      {
        // Encountered next number section, empty value into output
        if (inSperator) {
          output += currentSum;
          currentSum = 0;
          inSperator = false;
        }
        
        currentSum += token.value;
      }
      else if (token.type == TokenType.Multiplier)
      {
        currentSum *= token.value;
        if (token.value != 100)
        {
          inSperator = true;
        }
      }
    }
    // one last empty
    return output + currentSum;
  }

  private NumToken[] GetTokens(string number)
  {
    List<NumToken> tokens = new();
    var nums = number.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    foreach(var word in nums)
    {
      if (numberTable.ContainsKey(word))
      {
        tokens.Add(new NumToken(TokenType.Number, numberTable[word]));
      }
      else if (multiplierTable.ContainsKey(word))
      {
        tokens.Add(new NumToken(TokenType.Multiplier, multiplierTable[word]));
      }
      else
      {
        Console.WriteLine("Invalid phrase found in input");
        Environment.Exit(1);
      }
    }

    return tokens.ToArray();
  }
}