using Do_Kolokwium_02.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Do_Kolokwium_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FilmsDg.AutoGenerateColumns = false;
            LoadFilms();
        }

        private void LoadFilms()
        {
            using (FilmsDbContext dbContext = new FilmsDbContext())
            {
                var films = dbContext.Films.Include("Reviews").ToList();
                FilmsDg.ItemsSource = films;
            }
            Debug.WriteLine(Test());
        }


        private string Test()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = System.IO.Path.Combine("Database", "Films.mdf");
            string absolutePath = System.IO.Path.Combine(appDirectory, relativePath);
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{absolutePath}\";Integrated Security=True";
        }

        private void addFilmMi_Click(object sender, RoutedEventArgs e)
        {
            AddFilmWindow addFilmWindow = new AddFilmWindow();
            if (addFilmWindow.ShowDialog() == true)
            {
                LoadFilms();
            }
        }

        private void addReviewMi_Click(object sender, RoutedEventArgs e)
        {
            if (FilmsDg.SelectedItem is Film selectedFilm)
            {
                AddReviewWindow addGradeWindow = new AddReviewWindow(selectedFilm);
                if (addGradeWindow.ShowDialog() == true)
                {
                    LoadFilms();
                }
            }
            else
            {
                MessageBox.Show("Film nie został wybrany!");
                return;
            }
        }
    }
}
