
using System.CommandLine;

var inputArgument = new Argument<string>(
  name: "expression",
  description: "The expression to calculate",
  getDefaultValue: () => {return "";}
);

var rootCommand = new RootCommand("Calculates the math expression and outputs the result");
rootCommand.AddArgument(inputArgument);
rootCommand.SetHandler(CalculatorHandler, inputArgument);

await rootCommand.InvokeAsync(args);

void CalculatorHandler(string input)
{
  if (input == "")
  {
    Console.WriteLine("Please input a math expression");
    return;
  }
  Console.WriteLine($"{input}");
}