
if (args.Length == 0)
{
  Console.WriteLine("Please provide a number phrase as string");
  return;
}

if (args.Length > 1)
{
  Console.WriteLine("Argument should only contain one number phrase as string");
  return;
}

NumParser parser = new();
long outcome = parser.GetNumber(args[0]);
Console.WriteLine($"{outcome}");