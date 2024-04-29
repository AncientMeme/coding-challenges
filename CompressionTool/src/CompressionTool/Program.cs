using System.CommandLine;

// Setup command line tool
var fileArgument = new Argument<FileInfo?>(
  name: "file",
  description: "The target file"
);
var outputArgument = new Argument<string?>(
  name: "output",
  description: "The output file"
);

// Sub commands
var encodeCommand = new Command("encode", "Compress target file using Huffman encoding");
encodeCommand.AddArgument(fileArgument);
encodeCommand.AddArgument(outputArgument);
encodeCommand.SetHandler(CompressFile, fileArgument, outputArgument);
var decodeCommand = new Command("decode", "Decode compressed file using Huffman encoding");
decodeCommand.AddArgument(fileArgument);
decodeCommand.AddArgument(outputArgument);
decodeCommand.SetHandler(DecompressFile, fileArgument, outputArgument);

// Root command
var rootCommand = new RootCommand("Command line tool to compress and decompress files")
{
  encodeCommand,
  decodeCommand,
};

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

  // Read file content
  string content;
  using(FileStream file = info.OpenRead())
  using(StreamReader sr = new(file))
  {
    content = sr.ReadToEnd();
  }

  // Compress File
  var manager = new CompressionManager();
  manager.Compress(content, outputName);
}

void DecompressFile(FileInfo? info, string? outputName)
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

  // Get file stream
  using(FileStream stream = info.OpenRead())
  {
     // Decompress File
    var manager = new CompressionManager();
    manager.Decompress(stream, outputName);
  }
}
