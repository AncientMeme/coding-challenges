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
  // Handle no file or '-'
  if (fileName == null || fileName.Equals("-"))
  {
    Console.WriteLine("No file provided");
    Environment.Exit(1);
  }

  // Read the file
  FileStream file = File.OpenRead(fileName);
  string content;
  using(StreamReader reader = new(file))
  {
    content = reader.ReadToEnd();
  }

  // Handle Options
  bool noOptions = false;
  if (!countByte && !countChar && !countLine && !countWord)
  {
    noOptions = true;
  }

  string finalOutput = "";
  if (countLine || noOptions)
  {
    finalOutput += $"{Counter.GetLines(content)} ";
  }
  if (countWord || noOptions)
  {
    finalOutput += $"{Counter.GetWords(content)} ";
  }
  if (countChar || noOptions)
  {
    finalOutput += $"{Counter.GetChars(content)} ";
  }
  if (countByte)
  {
    finalOutput += $"{Counter.GetBytes(file)} ";
  }
  finalOutput += fileName;

  Console.WriteLine(finalOutput);
  file.Close();
}