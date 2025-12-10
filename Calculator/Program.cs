using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Prompt for history display or calculation
            Console.WriteLine(
                "Type \"h\" to see a history of calculations, \"d\" to delete history, or \"c\" to perform a calculation. Then press Enter."
            );

            string? modeSelection = "";
            while (modeSelection == null || !Regex.IsMatch(modeSelection, "[h|c|d]"))
            {
                modeSelection = Console.ReadLine();
            }

            if (modeSelection == "h")
            {
                calculator.DisplayHistory();
            }
            else if (modeSelection == "d")
            {
                calculator.ClearHistory();
            }
            else
            {
                // Ask the user to type the first number.
                double firstOperand = GetOperandFromUser(calculator);

                // Ask the user to type the second number
                double secondOperand = GetOperandFromUser(calculator);

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\tmax - Max");
                Console.WriteLine("\tmin - Min");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|max|min]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        double result = calculator.DoOperation(firstOperand, secondOperand, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine(
                                "This operation will result in a mathematical error.\n"
                            );
                        }
                        else
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(
                            "Oh no! An exception occurred trying to do the math.\n - Details: "
                                + e.Message
                        );
                    }
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write(
                "Press 'n' and Enter to close the app, or press any other key and Enter to continue: "
            );
            if (Console.ReadLine() == "n")
                endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }

    static double GetOperandFromUser(Calculator calculator)
    {
        while (true)
        {
            Console.WriteLine(
                "Type a number, and then press Enter. You can also type \"#number\" to use a result from your history: "
            );
            string? input = Console.ReadLine();

            if (input != null && input.StartsWith('#'))
            {
                if (!Int32.TryParse(input[1..], out int index))
                {
                    Console.WriteLine(
                        "Incorrect history format specified. Please use \"#number\"."
                    );
                    continue;
                }
                else
                {
                    // User will provide index with 1-based indexing, but we need 0-based
                    if (!calculator.GetResultFromHistory(index - 1, out double result))
                    {
                        Console.WriteLine(
                            "Given entry did not correspond to a history entry; please enter an existing entry index."
                        );
                        continue;
                    }
                    return result;
                }
            }
            else
            {
                if (!double.TryParse(input, out double result))
                {
                    Console.WriteLine("This is not valid input. Please enter a numeric value.");
                    continue;
                }
                return result;
            }
        }
    }
}
