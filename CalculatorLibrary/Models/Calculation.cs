namespace CalculatorLibrary
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
    }
}
