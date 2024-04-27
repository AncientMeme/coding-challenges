using System.CommandLine;

// Setup command line tool
var fileArgument = new Argument<FileInfo?>(
  name: "file",
  description: "The file to be compressed"
);

var rootCommand = new RootCommand("Command line tool to compress files");
rootCommand.AddArgument(fileArgument);

rootCommand.SetHandler(CompressFile, fileArgument);
await rootCommand.InvokeAsync(args);

// Run main function
void CompressFile(FileInfo? info) 
{
  if (info == null)
  {
    Console.WriteLine("File argument cannot be null");
    return;
  }
  if (!info.Exists)
  {
    Console.WriteLine("The file was not found");
    return;
  }

  string fileContent; 
  using (FileStream stream = info.OpenRead())
  using (StreamReader sr = new(stream))
  {
    fileContent = sr.ReadToEnd();
  }
}
