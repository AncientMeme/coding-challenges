
using System.CommandLine;

var fileArgument = new Argument<string[]?>(
  name: "files",
  description: "The files to concatenate and output content",
  getDefaultValue: () => null
);

var rootCommand = new RootCommand("Concatenate the content of files and output to standard out");
rootCommand.AddArgument(fileArgument);
rootCommand.SetHandler(CatHandler, fileArgument);
await rootCommand.InvokeAsync(args);

void CatHandler(string[]? files)
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

  // Output
  foreach (string line in content)
  {
    Console.WriteLine(line);
  }
}