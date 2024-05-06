
using System.CommandLine;

var fileArgument = new Argument<string[]?>(
  name: "files",
  description: "The files to concatenate and output content",
  getDefaultValue: () => null
);

var numberOption = new Option<bool>(
  name: "--number",
  description: "The option to add numbers before lines",
  getDefaultValue: () => false
);
numberOption.AddAlias("-n");

var blankOption = new Option<bool>(
  name: "--blank",
  description: "The option to add numbers for non blank lines",
  getDefaultValue: () => false
);
blankOption.AddAlias("-b");


var rootCommand = new RootCommand("Concatenate the content of files and output to standard out");
rootCommand.AddArgument(fileArgument);
rootCommand.AddOption(numberOption);
rootCommand.AddOption(blankOption);
rootCommand.SetHandler(CatHandler, fileArgument, numberOption, blankOption);
await rootCommand.InvokeAsync(args);

void CatHandler(string[]? files, bool addLineNum, bool ignoreBlank)
{
  // Get Content
  string[] content;
  if (files == null || (files.Length == 1 && files[0].Equals("-")))
  {
    content = Cat.GetStandardInContent();
  }
  else
  {
    content = Cat.GetFileContent(files);
  }

  // Add line numbers if enabled
  if (addLineNum || ignoreBlank)
  {
    content = Cat.AddLineNumber(content, ignoreBlank);
  }

  // Output
  foreach (string line in content)
  {
    Console.WriteLine(line);
  }
}