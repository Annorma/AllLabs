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
        #region Comments
        //public IList<Student> Students { get; set; }
        //private readonly GradesConverter gradesConverter = new GradesConverter();

        //public class GradesConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if (value is not List<Grade> grades) return null;

        //        return string.Join("; ", grades.Select(g => $"{g.Subject}: {g.Value}"));
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        #endregion

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

        #region Comments
        //Students = new List<Student>
        //{
        //    new Student(){FirstName = "Jan", LastName="Kowalski", Faculty="WIMII", StudentNo = 116843, Grades = new List<Grade>(){
        //        new Grade("PO",4.5),
        //        new Grade("AM",3.5) }
        //    },
        //    new Student(){FirstName = "Michał", LastName="Nowak", Faculty="WIMII", StudentNo = 468735, Grades = new List<Grade>()
        //    {
        //        new Grade("PO",4.5),
        //        new Grade("LM",5.0) }
        //    },
        //    new Student(){FirstName = "Marcin", LastName="Jakubski", Faculty="WIP", StudentNo = 647674, Grades = new List<Grade>()
        //    {
        //        new Grade("GH",2.0),
        //        new Grade("WF",5.0) }
        //    }
        //};
        //StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Imię", Binding = new Binding("FirstName") });
        //StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Nazwisko", Binding = new Binding("LastName") });
        //StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Wydział", Binding = new Binding("Faculty") });
        //StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Nr albumu", Binding = new Binding("StudentNo") });
        //StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Oceny", Binding = new Binding("Grades") { Converter = new GradesConverter() } });
        //StudentsDg.AutoGenerateColumns = false;
        //StudentsDg.ItemsSource = Students;
        //StudentsDg.IsReadOnly = true;
        //StudentsDg.IsReadOnly = true;
        #endregion
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
                        // Usuń oceny studenta
                        foreach (var grade in studentToRemove.Grades.ToList())
                        {
                            _gradeRepository.Delete(grade);
                        }
                    }

                    _studentRepository.Delete(studentToRemove);
                    RefreshGrid();
                    ApplyDateOfBirthFilter();
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
