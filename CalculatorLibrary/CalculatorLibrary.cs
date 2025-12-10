using System.Diagnostics;
using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        List<Calculation> calculations = new();

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            Trace.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Division");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            calculations.Add(new Calculation(num1, num2, result, op));

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();

            Console.WriteLine($"Calculator performed {calculations.Count()} calculation(s) on this run.");
        }

        public void DisplayHistory()
        {
            if (calculations.Count() == 0)
            {
                Console.WriteLine("No calculations performed!");
                return;
            }

            for (int i = 0; i < calculations.Count(); i++)
            {
                Console.WriteLine($"#{i + 1}: {calculations[i]}");
            }
        }

        public void ClearHistory()
        {
            calculations.Clear();
            Console.WriteLine("History cleared.");
        }

        public bool GetResultFromHistory(int index, out double result)
        {
            if ((index < 0) || index >= calculations.Count())
            {
                result = 0;
                return false;
            }
            else
            {
                result = calculations[index].GetResult();
                return true;
            }
        }
    }
}
