using System.CommandLine;

// Setup command line tool
var fileArgument = new Argument<FileInfo?>(
  name: "file",
  description: "The file to be compressed"
);
var outputArgument = new Argument<string?>(
  name: "output",
  description: "The name of the compressed file"
);

// Sub commands
var encodeCommand = new Command("encode", "Compress target file using Huffman encoding");
encodeCommand.AddArgument(fileArgument);
encodeCommand.AddArgument(outputArgument);
encodeCommand.SetHandler(CompressFile, fileArgument, outputArgument);

// Root command
var rootCommand = new RootCommand("Command line tool to compress and decompress files");
rootCommand.Add(encodeCommand);

await rootCommand.InvokeAsync(args);

// Run main function
void CompressFile(FileInfo? info, string? outputName) 
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
  if (outputName == null)
  {
    Console.WriteLine("Output file required");
    return;
  }

  // Read File
  string fileContent; 
  using (FileStream stream = info.OpenRead())
  using (StreamReader sr = new(stream))
  {
    fileContent = sr.ReadToEnd();
  }

  // Compress File
  var manager = new CompressionManager();
  manager.CompressFile(fileContent, outputName);
}
