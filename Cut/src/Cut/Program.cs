using System.CommandLine;

var fieldOption = new Option<string>(
  name: "--field",
  description: "The field to select, also prints lines without delimiter characters"
);
fieldOption.AddAlias("-f");

var rootCommand = new RootCommand("A tool for cutting selected portion from each line in a file");
rootCommand.AddOption(fieldOption);
rootCommand.SetHandler(Cut, fieldOption);

await rootCommand.InvokeAsync(args);

void Cut(string fields)
{
  // Parse the selected fields
  List<int> selectedFields = new();
  char[] seperators = {' ', ','};
  string[] fieldEntries = fields.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
  foreach (string entry in fieldEntries)
  {
    selectedFields.Add(Convert.ToInt32(entry));
  }
}
