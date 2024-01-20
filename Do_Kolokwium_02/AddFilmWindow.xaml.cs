using Do_Kolokwium_02.Classes;
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

namespace Do_Kolokwium_02
{
    /// <summary>
    /// Interaction logic for AddFilmWindow.xaml
    /// </summary>
    public partial class AddFilmWindow : Window
    {
        public Film Film { get; set; }

        public AddFilmWindow(Film film = null)
        {
            InitializeComponent();
            
            if (film != null)
            {
                Film = new Film
                {
                    Name = film.Name,
                    Description = film.Description,
                    Genre = film.Genre,
                    Budget = film.Budget,
                    Reviews = new List<Review>(film.Reviews),
                };

                NameTb.Text = Film.Name;
                DescriptionTb.Text = Film.Name;
                GenreTb.Text = Film.Genre;
                BudgetTb.Text = Film.Budget.ToString();
                DatePck.SelectedDate = Film.CreationDate;
            }
            else
            {
                Film = new Film();
                Film.Reviews = new List<Review>();
            };
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (
                 !Regex.IsMatch(NameTb.Text, @"^[\p{Lu}\p{Ll}\d\s',.!?&]{1,50}$") ||
                 !Regex.IsMatch(GenreTb.Text, @"^\p{Lu}\p{Ll}{1,30}$") ||
                 !Regex.IsMatch(BudgetTb.Text, @"^[0-9]{1,20}$") ||
                 DatePck.SelectedDate > DateTime.Now
                 )
            {
                MessageBox.Show("Wprowadzone dane są niepoprawne!");
                return;
            };
            Film.Name = NameTb.Text;
            Film.Description = DescriptionTb.Text;
            Film.Budget = int.Parse(BudgetTb.Text);
            Film.Genre = GenreTb.Text;
            Film.CreationDate = DatePck.SelectedDate.Value;

            using (FilmsDbContext dbContext = new FilmsDbContext())
            {
                dbContext.Films.Add(Film);
                dbContext.SaveChanges();
            }

            DialogResult = true;
        }
    }
}
