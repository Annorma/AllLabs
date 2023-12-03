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
                throw new ArgumentException("Cannot divide by zero.");
            }
        }

        // Ustawienie operacji
        public void SetOperation(Func<double, double, double> op)
        {
            operation = op;
        }
    }
}
