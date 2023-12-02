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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private double a = 0;
        private double b = 0;
        private int count = 0;

        private void Calculate()
        {
            MathOperations mathOperations = MathOperations.Instance;
            
            if (resultTextBox.Text != "" && calculationsTextBox.Text != "" || resultTextBox.Text != null && calculationsTextBox != null)
            {
                switch (count)
                {
                    case 1:
                        b = double.Parse(resultTextBox.Text);
                        resultTextBox.Text = mathOperations.Plus(a, b).ToString();
                        break;
                    case 2:
                        b = double.Parse(resultTextBox.Text);
                        resultTextBox.Text = mathOperations.Minus(a, b).ToString();
                        break;
                    case 3:
                        b = double.Parse(resultTextBox.Text);
                        resultTextBox.Text = mathOperations.Multiply(a, b).ToString();
                        break;
                    case 4:
                        b = double.Parse(resultTextBox.Text);
                        resultTextBox.Text = mathOperations.Divide(a, b).ToString();
                        break;

                    default:
                        break;
                }
            }
            else { resultTextBox.Text = "Error"; }
        }

        private void oneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "1";
            }
            else { resultTextBox.Text += "1"; }
        }

        private void twoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "2";
            }
            else { resultTextBox.Text += "2"; }
        }

        private void threeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "3";
            }
            else { resultTextBox.Text += "3"; }
        }

        private void fourBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "4";
            }
            else { resultTextBox.Text += "4"; }
        }

        private void fiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "5";
            }
            else { resultTextBox.Text += "5"; }
        }

        private void sixBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "6";
            }
            else { resultTextBox.Text += "6"; }
        }

        private void sevenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "7";
            }
            else { resultTextBox.Text += "7"; }
        }

        private void eightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "8";
            }
            else { resultTextBox.Text += "8"; }
        }

        private void nineBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "9";
            }
            else { resultTextBox.Text += "9"; }
        }

        private void zeroBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text = "0";
            }
            else { resultTextBox.Text += "0"; }
        }

        private void dotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultTextBox.Text == "0" && resultTextBox != null)
            {
                resultTextBox.Text += ",";
            }
            else { resultTextBox.Text += ","; }
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
            if (resultTextBox.Text != "0" && resultTextBox.Text != "")
            {
                resultTextBox.Text = resultTextBox.Text.Remove(resultTextBox.Text.Length - 1);
            }
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            a = double.Parse(resultTextBox.Text);
            resultTextBox.Clear();
            count = 1;
            calculationsTextBox.Text += a.ToString() + "+";
        }

        private void minusBtn_Click(object sender, RoutedEventArgs e)
        {
            a = double.Parse(resultTextBox.Text);
            resultTextBox.Clear();
            count = 2;
            calculationsTextBox.Text += a.ToString() + "-";
        }

        private void multiplyBtn_Click(object sender, RoutedEventArgs e)
        {
            a = double.Parse(resultTextBox.Text);
            resultTextBox.Clear();
            count = 3;
            calculationsTextBox.Text += a.ToString() + "*";
        }

        private void divideBtn_Click(object sender, RoutedEventArgs e)
        {
            a = double.Parse(resultTextBox.Text);
            resultTextBox.Clear();
            count = 4;
            calculationsTextBox.Text += a.ToString() + "/";
        }

        private void equalBtn_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
            calculationsTextBox.Text = "";
        }
    }
}
