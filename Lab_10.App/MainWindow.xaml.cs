using Lab_10.DAL;
using Lab_10.Model;
using Lab_10.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

namespace Lab_10.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Grade> _gradeRepository;
        private ICollectionView _collectionView;

        public MainWindow()
        {
            var connString = Application.Current.Resources["connString"] as string;
            _studentRepository = new Repository<Student>(connString);
            _gradeRepository = new Repository<Grade>(connString);
            InitializeComponent();
            RefreshGrid();
            _collectionView = CollectionViewSource.GetDefaultView(StudentsDg.ItemsSource);
        }

        private void RefreshGrid()
        {
            var studentList = _studentRepository.Select();
            foreach (var student in studentList)
            {
                student.Grades = _gradeRepository.Select(new Tuple<string, string, object, string>("StudentNo", "=", student.StudentNo, null));
                //Debug.WriteLine($"Student: {student.FirstName} {student.LastName}, Grades Count: {student.Grades.Count}");
                //Debug.WriteLine($"Student: {student.FirstName} {student.LastName}, Grades: {student.JoinedGrades}");
                //student.JoinedGrades = gradesConverter.Convert(student.Grades, typeof(string), null, CultureInfo.InvariantCulture) as string;
            }
            SetGrid(studentList);
            ApplyDateOfBirthFilter();
            _collectionView = CollectionViewSource.GetDefaultView(StudentsDg.ItemsSource);
        }
        private void SetGrid<T>(List<T> list) where T : new()
        {
            Type type = typeof(T);
            if (type.GetCustomAttribute<DbTabAttribute>() == null) { return; }
            StudentsDg.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                var col = prop.GetCustomAttribute<DisplayGridAttribute>();
                if (col != null)
                {
                    StudentsDg.Columns.Add(new DataGridTextColumn()
                    {
                        Header = col.Title ?? prop.Name,
                        Binding = new Binding(prop.Name)
                    });
                }
            }
            StudentsDg.ItemsSource = list;
            StudentsDg.AutoGenerateColumns = false;
            StudentsDg.Items.Refresh();
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

        private void AddMi_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow;
            if (StudentsDg.SelectedItem != null && StudentsDg.SelectedItem is Student student)
            {
                addStudentWindow = new AddStudentWindow(student);
            }
            else
            {
                addStudentWindow = new AddStudentWindow();
            }

            if (addStudentWindow.ShowDialog() == true)
            {
                Student updatedStudent = addStudentWindow.Student;

                if (StudentsDg.SelectedItem != null)
                {
                    _studentRepository.Update(updatedStudent);
                }
                else
                {
                    _studentRepository.Insert(updatedStudent);
                }
                RefreshGrid();
                ApplyDateOfBirthFilter();
                _collectionView.Refresh();
            }
        }

        private void RemoveMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student studentToRemove)
            {
                string confirmationMessage = studentToRemove.HasGrades() ? "Ten student ma przypisane oceny. Czy chcesz usunąć również oceny?" : "Czy na pewno chcesz usunąć tego studenta?";

                MessageBoxResult result = MessageBox.Show(confirmationMessage, "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (studentToRemove.HasGrades())
                    {
                        foreach (var grade in studentToRemove.Grades.ToList())
                        {
                            _gradeRepository.Delete(grade);
                        }
                    }

                    _studentRepository.Delete(studentToRemove);
                    RefreshGrid();
                    ApplyDateOfBirthFilter();
                    _collectionView.Refresh();
                }
            }
        }
        private void AddGradeMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student selectedStudent)
            {
                AddGradeWindow addGradeWindow = new AddGradeWindow(selectedStudent, _gradeRepository);
                addGradeWindow.ShowDialog();
                RefreshGrid();
            }
        }

        private void exitMi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
