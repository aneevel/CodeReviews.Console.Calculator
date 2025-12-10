namespace CalculatorLibrary.Models
{
    internal class Calculation
    {
        double FirstOperand;
        double SecondOperand;
        double Result;
        string OperatorPerformed;

        public Calculation(
            double firstOperand,
            double secondOperand,
            double result,
            string operatorPerformed
        )
        {
            this.FirstOperand = firstOperand;
            this.SecondOperand = secondOperand;
            this.Result = result;
            this.OperatorPerformed = operatorPerformed;
        }

        public override string ToString()
        {
            return $"{FirstOperand} {CalculationExtensions.OperatorToSymbol(OperatorPerformed)} {SecondOperand} = {Result}";
        }

        public double GetResult()
        {
            return Result;
        }

    }

    class CalculationExtensions
    {
        public static string OperatorToSymbol(string opcode)
        {
            switch (opcode)
            {
                case "a":
                    return "+";
                case "s":
                    return "-";
                case "m":
                    return "*";
                case "d":
                    return "/";
                case "p":
                    return "^";
                default:
                    throw new System.ArgumentException("Invalid operator string provided!");
            }
        }
    }
}
