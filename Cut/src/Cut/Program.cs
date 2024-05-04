using System.CommandLine;

var fieldOption = new Option<int>(
  name: "--field",
  description: "The field to select, also prints lines without delimiter characters"
);
var rootCommand = new RootCommand("A tool for cutting selected portion from each line in a file");
