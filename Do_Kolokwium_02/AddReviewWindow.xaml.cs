using Do_Kolokwium_02.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddReviewWindow.xaml
    /// </summary>
    public partial class AddReviewWindow : Window
    {
        private readonly Film selectedFilm;
        public AddReviewWindow(Film film = null)
        {
            InitializeComponent();
            selectedFilm = film;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(RatingTb.Text, @"^(?:10|\d(?:\.\d)?)$"))
                {
                    MessageBox.Show("Wprowadzone dane są niepoprawne!");
                    return;
                }

                string description = DescriptionTb.Text;
                double rating = double.Parse(RatingTb.Text, CultureInfo.InvariantCulture);

                Review newReview = new Review
                {
                    Rating = rating,
                    Description = description,
                    FilmId = selectedFilm.Id
                };

                using (FilmsDbContext dbContext = new FilmsDbContext())
                {
                    dbContext.Reviews.Add(newReview);
                    dbContext.SaveChanges();
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd dodawania oceny: {ex.Message}\n\n");
            }
        }
    }
}
