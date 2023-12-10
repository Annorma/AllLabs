using System;

namespace Lab_07.BLL
{
    public class MathOperations
    {
        private static MathOperations? _instance;

        private MathOperations() { }

        public static MathOperations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MathOperations();
                }
                return _instance;
            }
        }

        // Delegat do przechowywania referencji do funkcji matematycznych
        private Func<double, double, double> operation;

        public double PerformOperation(double a, double b)
        {
            return operation(a, b);
        }

        public double Plus(double a, double b) => a + b;
        public double Minus(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b != 0)
            {
                return a / b;
            }
            else
            {
                throw new DivideByZeroException();
            }
        }

        public double Factorial(double n)
        {
            if (n < 0)
                return double.NaN; // Nieobsługiwane dla liczb ujemnych
            else if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        public double Absolute(double x) => Math.Abs(x);

        public double SquareRoot(double x) => Math.Sqrt(x);

        public double Power(double x, double exponent) => Math.Pow(x, exponent);

        public double Percentage(double baseValue, double percentage) => baseValue * (percentage / 100);

        public double ChangeSign(double value) => -value;


        // Ustawienie operacji
        public void SetOperation(Func<double, double, double> op)
        {
            operation = op;
        }
    }
}
