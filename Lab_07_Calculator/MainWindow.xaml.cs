using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab_07.BLL;

namespace Lab_07_Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += Window_PreviewKeyDown;
        }

        private MathOperations mathOperations = MathOperations.Instance;
        private double a = 0;
        private double b = 0;

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string? buttonText = button.Content.ToString();

            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = buttonText;
            }
            else if (resultTextBox != null)
            {
                resultTextBox.Text += buttonText;
            }
        }

        private void ceBtn_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = "0";
        }

        private void cBtn_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = "0";
            calculationsTextBox.Text = "";
            a = 0;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(resultTextBox.Text))
            {
                resultTextBox.Text = resultTextBox.Text.Substring(0, resultTextBox.Text.Length - 1);
            }
        }

        private bool TryParseDouble(string input, out double result)
        {
            return double.TryParse(input, out result);
        }

        private void OperatorButtonClick(Func<double, double, double> operation, string symbol)
        {
            if (TryParseDouble(resultTextBox.Text, out double parsedValue))
            {
                a = parsedValue;
                resultTextBox.Clear();
                calculationsTextBox.Text += a.ToString() + symbol;

                // Ustawienie odpowiedniej operacji w MathOperations
                mathOperations.SetOperation(operation);
            }
            else
            {
                resultTextBox.Text = "Error";
            }
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e) => OperatorButtonClick(mathOperations.Plus, "+");
        private void minusBtn_Click(object sender, RoutedEventArgs e) => OperatorButtonClick(mathOperations.Minus, "-");
        private void multiplyBtn_Click(object sender, RoutedEventArgs e) => OperatorButtonClick(mathOperations.Multiply, "*");
        private void divideBtn_Click(object sender, RoutedEventArgs e) => OperatorButtonClick(mathOperations.Divide, "/");

        private void equalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TryParseDouble(resultTextBox.Text, out double parsedValue))
            {
                b = parsedValue;
                resultTextBox.Text = mathOperations.PerformOperation(a, b).ToString();
                calculationsTextBox.Text = "";
                a = 0;
            }
            else
            {
                resultTextBox.Text = "Error";
            }
        }

        //Umożliwia wpisywanie liczb i znaków z klawiatury
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    AppendCharacter("0");
                    break;

                case Key.D1:
                case Key.NumPad1:
                    AppendCharacter("1");
                    break;

                case Key.D2:
                case Key.NumPad2:
                    AppendCharacter("2");
                    break;

                case Key.D3:
                case Key.NumPad3:
                    AppendCharacter("3");
                    break;

                case Key.D4:
                case Key.NumPad4:
                    AppendCharacter("4");
                    break;

                case Key.D5:
                case Key.NumPad5:
                    AppendCharacter("5");
                    break;

                case Key.D6:
                case Key.NumPad6:
                    AppendCharacter("6");
                    break;

                case Key.D7:
                case Key.NumPad7:
                    AppendCharacter("7");
                    break;

                case Key.D8:
                case Key.NumPad8:
                    AppendCharacter("8");
                    break;

                case Key.D9:
                case Key.NumPad9:
                    AppendCharacter("9");
                    break;

                case Key.OemPeriod:
                case Key.Decimal:
                    AppendCharacter(",");
                    break;

                case Key.Add:
                    OperatorButtonClick(MathOperations.Instance.Plus, "+");
                    break;

                case Key.Subtract:
                    OperatorButtonClick(MathOperations.Instance.Minus, "-");
                    break;

                case Key.Multiply:
                    OperatorButtonClick(MathOperations.Instance.Multiply, "*");
                    break;

                case Key.Divide:
                    OperatorButtonClick(MathOperations.Instance.Divide, "/");
                    break;

                case Key.Enter:
                    equalBtn_Click(sender, e);
                    break;

                case Key.Back:
                    deleteBtn_Click(sender, e);
                    break;
            }
        }

        private void AppendCharacter(string character)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = character;
            }
            else
            {
                resultTextBox.Text += character;
            }
        }
    }
}
