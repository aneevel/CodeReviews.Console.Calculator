namespace CalculatorLibrary.Models
{
    class Calculation
    {
        double firstOperand;
        double secondOperand;
        double result;
        string operatorPerformed;

        public Calculation(double firstOperand, double secondOperand, double result, string operatorPerformed)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
            this.result = result;
            this.operatorPerformed = operatorPerformed;
        }

        public override string ToString()
        {
            return $"{firstOperand} {CalculationExtensions.OperatorToSymbol(operatorPerformed)} {secondOperand} = {result}";
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
                    default:
                        throw new System.ArgumentException("Invalid operator string provided!");

                }
            }
        }
    }
}
