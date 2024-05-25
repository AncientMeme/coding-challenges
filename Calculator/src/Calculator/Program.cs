
using System.CommandLine;
using Util;

var inputArgument = new Argument<string>(
  name: "expression",
  description: "The expression to calculate, wrap it in double quotes, i.e. \"3 + 5\"",
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

  Lexer lexer = new(input);
  Queue<Token> tokens = new();
  try 
  {
    tokens = lexer.GetTokens();
  }
  catch (InvalidDataException e)
  {
    Console.WriteLine(e.Message);
    Environment.Exit(1);
  }

  Parser parser = new(tokens);
  double answer = 0;
  try
  {
    answer = parser.GetAnswer();
  }
  catch(Exception e)
  {
    Console.WriteLine(e.Message);
    Environment.Exit(1);
  }

  Console.WriteLine($"{answer}");
}