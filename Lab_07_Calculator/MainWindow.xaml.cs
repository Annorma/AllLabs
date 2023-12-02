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

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void divideBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void multiplyBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void minusBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void oneBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "1";
            }
            else { rezultTextBox.Text += "1"; }
        }

        private void twoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "2";
            }
            else { rezultTextBox.Text += "2"; }
        }

        private void threeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "3";
            }
            else { rezultTextBox.Text += "3"; }
        }

        private void fourBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "4";
            }
            else { rezultTextBox.Text += "4"; }
        }

        private void fiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "5";
            }
            else { rezultTextBox.Text += "5"; }
        }

        private void sixBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "6";
            }
            else { rezultTextBox.Text += "6"; }
        }

        private void sevenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "7";
            }
            else { rezultTextBox.Text += "7"; }
        }

        private void eightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "8";
            }
            else { rezultTextBox.Text += "8"; }
        }

        private void nineBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "9";
            }
            else { rezultTextBox.Text += "9"; }
        }

        private void zeroBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text = "0";
            }
            else { rezultTextBox.Text += "0"; }
        }

        private void dotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rezultTextBox.Text == "0" && rezultTextBox != null)
            {
                rezultTextBox.Text += ",";
            }
            else { rezultTextBox.Text += ","; }
        }

        private void ceBtn_Click(object sender, RoutedEventArgs e)
        {
            rezultTextBox.Text = "0";
        }

        private void cBtn_Click(object sender, RoutedEventArgs e)
        {
            rezultTextBox.Text = "0";
            calculationsTextBox.Text = "";
        }

        private void equalBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
