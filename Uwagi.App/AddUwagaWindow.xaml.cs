using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Uwagi.App
{
    /// <summary>
    /// Interaction logic for AddUwagaWindow.xaml
    /// </summary>
    public partial class AddUwagaWindow : Window
    {
        public Uwaga Uwago { get; set; }

        public AddUwagaWindow(Uwaga uwaga = null)
        {
            InitializeComponent();

            if (uwaga != null)
            {
                Uwago = new Uwaga
                {
                    Linia = uwaga.Linia,
                    Wartosc = uwaga.Wartosc,

                };

                LiniaTb.Text = Uwago.Linia.ToString();
                UwagaTb.Text = Uwago.Wartosc;

            }
            else
            {
                Uwago = new Uwaga();
            };

        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Uwago.Linia = int.Parse(LiniaTb.Text);
            Uwago.Wartosc = UwagaTb.Text;

            using (UwagiDbContext dbContext = new UwagiDbContext())
            {
                dbContext.Uwagi.Add(Uwago);
                dbContext.SaveChanges();
            }

            DialogResult = true;
        }
    }
}
