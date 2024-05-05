using System.CommandLine;

var fileArgument = new Argument<string?>(
  name: "file",
  description: "The file to read from"
);
var fieldOption = new Option<string>(
  name: "--field",
  description: "The field to select, also prints lines without delimiter characters"
);
var delimiterOption = new Option<char>(
  name: "--delimiter",
  description: "The character to seperate fields",
  getDefaultValue: () => '\t'
);

fieldOption.AddAlias("-f");
delimiterOption.AddAlias("-d");

var rootCommand = new RootCommand("A tool for cutting selected portion from each line in a file");
rootCommand.AddArgument(fileArgument);
rootCommand.AddOption(fieldOption);
rootCommand.AddOption(delimiterOption);
rootCommand.SetHandler(Cut, fileArgument, fieldOption, delimiterOption);

await rootCommand.InvokeAsync(args);

void Cut(string? file, string fields, char delimiter)
{
  // File to read from
  string[]? entries = InputParser.GetEntries(file);
  if (entries == null)
  {
    Console.WriteLine("Unable to read content");
    return;
  }

  // Parse the selected fields
  int[] selectedFields = InputParser.GetFields(fields);
  
  // Cut out the selected fields
  string[] output = Cutter.Cut(entries, selectedFields, delimiter);

  // Output the results
  foreach(var line in output)
  {
    Console.WriteLine(line);
  }
}