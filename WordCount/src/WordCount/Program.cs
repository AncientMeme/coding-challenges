using System.CommandLine;

var fileArgument = new Argument<string?>(
  name: "file",
  description: "File to read from",
  getDefaultValue: () => null
);
var byteOption = new Option<bool>(
  name: "--byte",
  description: "The number of bytes in a file"
);
var charOption = new Option<bool>(
  name: "--char",
  description: "The number of characters in a file"
);
var lineOption = new Option<bool>(
  name: "--line",
  description: "The number of newline characters in a file"
);
var wordOption = new Option<bool>(
  name: "--word",
  description: "The number of word in a file"
);

byteOption.AddAlias("-c");
charOption.AddAlias("-m");
lineOption.AddAlias("-l");
wordOption.AddAlias("-w");

var rootCommand = new RootCommand("Word count command line tool");
rootCommand.AddArgument(fileArgument);
rootCommand.AddOption(byteOption);
rootCommand.AddOption(charOption);
rootCommand.AddOption(lineOption);
rootCommand.AddOption(wordOption);
rootCommand.SetHandler(WordCountHandler, fileArgument, byteOption, charOption, lineOption, wordOption);
await rootCommand.InvokeAsync(args);

void WordCountHandler(string? fileName, bool countByte, bool countChar, bool countLine, bool countWord)
{
  // Get bytes and content from stream
  int bytes = 0;
  string content = "";
  if (fileName == null || fileName.Equals("-"))
  {
    // Handle no file or '-'
    using(Stream stdin = Console.OpenStandardInput())
    {
      var byteContent = GetStdinBytes(stdin);
      bytes = byteContent.Length;
      content = System.Text.Encoding.UTF8.GetString(byteContent);
    }
  }
  else
  {
    // Read provided file
    using(FileStream file = File.OpenRead(fileName))
    using(StreamReader reader = new(file))
    {
      bytes = Counter.GetBytes(file);
      content = reader.ReadToEnd();
    }
  }

  // Build Output
  bool noOptions = !countByte && !countChar && !countLine && !countWord;
  string finalOutput = "  ";
  if (countLine || noOptions)
  {
    finalOutput += $"{Counter.GetLines(content)} ";
  }
  if (countWord || noOptions)
  {
    finalOutput += $"{Counter.GetWords(content)} ";
  }
  if (countChar)
  {
    finalOutput += $"{Counter.GetChars(content)} ";
  }
  if (countByte || noOptions)
  {
    finalOutput += $"{bytes} ";
  }
  if (fileName != null && !fileName.Equals("-"))
  {
    finalOutput += fileName;
  }
  Console.WriteLine(finalOutput);
}

byte[] GetStdinBytes(Stream stdin)
{
  List<byte> bytes = new();
  int next;
  while((next = stdin.ReadByte()) != -1)
  {
    bytes.Add((byte)next);
  }
  return bytes.ToArray();
}