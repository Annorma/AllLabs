using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace Lab_11.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollectionView _collectionView;
        public MainWindow()
        {
            InitializeComponent();
            StudentsDg.AutoGenerateColumns = false;
            LoadStudents();
            _collectionView = CollectionViewSource.GetDefaultView(StudentsDg.ItemsSource);
        }

        private void LoadStudents()
        {
            using (StudentsDbContext dbContext = new StudentsDbContext())
            {
                var students = dbContext.Students.Include("Grades").ToList();
                StudentsDg.ItemsSource = students;
            }
            ApplyDateOfBirthFilter();
            _collectionView = CollectionViewSource.GetDefaultView(StudentsDg.ItemsSource);
        }

        private void AddMi_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = new AddStudentWindow();
            if (addStudentWindow.ShowDialog() == true)
            {
                LoadStudents();
                ApplyDateOfBirthFilter();
                _collectionView.Refresh();
            }
        }

        private void RemoveMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student selectedStudent)
            {
                string confirmationMessage = selectedStudent.HasGrades() ? "Ten student ma przypisane oceny. Czy chcesz usunąć również oceny?" : "Czy na pewno chcesz usunąć tego studenta?";

                MessageBoxResult result = MessageBox.Show(confirmationMessage, "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (StudentsDbContext dbContext = new StudentsDbContext())
                    {
                        var studentToRemove = dbContext.Students.Include("Grades").Single(s => s.StudentNo == selectedStudent.StudentNo);

                        if (studentToRemove != null)
                        {
                            if (studentToRemove.HasGrades())
                            {
                                foreach (var grade in studentToRemove.Grades.ToList())
                                {
                                    dbContext.Grades.Remove(grade);
                                }
                            }

                            dbContext.Students.Remove(studentToRemove);
                            dbContext.SaveChanges();
                            LoadStudents();
                            ApplyDateOfBirthFilter();
                            _collectionView.Refresh();
                        }
                    }
                }
            }
        }


        private void AddGradeMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student selectedStudent)
            {
                AddGradeWindow addGradeWindow = new AddGradeWindow(selectedStudent);
                if (addGradeWindow.ShowDialog() == true)
                {
                    LoadStudents();
                    ApplyDateOfBirthFilter();
                    _collectionView.Refresh();
                }
            }
        }
        private void exitMi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Filtrowanie

        private void ApplyDateOfBirthFilter()
        {
            if (_collectionView != null)
            {
                if (filterDatePck.SelectedDate.HasValue)
                {
                    DateTime selectedDate = filterDatePck.SelectedDate.Value.Date;
                    FilterByDateOfBirth(selectedDate);
                }
                else
                {
                    ClearDateOfBirthFilter();
                }
            }
        }

        private void FilterByDateOfBirth(DateTime selectedDate)
        {
            _collectionView.Filter = item =>
            {
                if (item is Student student)
                {
                    return student.DateOfBirth.Date == selectedDate.Date;
                }

                return false;
            };
        }

        private void ClearDateOfBirthFilter()
        {
            if (_collectionView != null)
            {
                _collectionView.Filter = null;
            }
        }

        private void filterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (filterDatePck.SelectedDate.HasValue)
            {
                DateTime selectedDate = filterDatePck.SelectedDate.Value.Date;
                FilterByDateOfBirth(selectedDate);
            }
            else
            {
                ClearDateOfBirthFilter();
            }
        }
        #endregion
    }
}
