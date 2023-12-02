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

        public double Plus(double a, double b)
        {
            return a + b;
        }

        public double Minus(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b != 0)
            {
                return a / b;
            }
            else
            {
                return -1;
            }
        }
    }
}
