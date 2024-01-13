using Lab_10.DAL;
using Lab_10.Model;
using Lab_10.Model.Attributes;
using System;
using System.Collections.Generic;
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
        public IList<Student> Students { get; set; }

        public class GradesConverter : IValueConverter
        {
            public object? Convert(object value, Type targetType, object parametr, CultureInfo culture)
            {
                if (value is not List<Grade> grades) return null;
                return string.Join(", ", grades.Select(g => $"{g.Subject}: {g.Value}"));
            }

            public object? ConvertBack(object value, Type targetType, object parametr, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Grade> _gradeRepository;

        public MainWindow()
        {
            var connString = Application.Current.Resources["connString"] as string;
            _studentRepository = new Repository<Student>(connString);
            _gradeRepository = new Repository<Grade>(connString);
            InitializeComponent();
            RefreshGrid();

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
                student.Grades = _gradeRepository.Select(new Tuple<string, string, object, string>("StudentNo", "=", student.StudentNo, null));
            SetGrid(studentList);
        }

        private void SetGrid<T>(List<T> list) where T : new()
        {
            Type type = typeof(T);
            if (type.GetCustomAttribute<DbTabAttribute>() == null)
                return;
            StudentsDg.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                var col = prop.GetCustomAttribute<DisplayGridAttribute>();
                if (col != null)
                    StudentsDg.Columns.Add(new DataGridTextColumn()
                    { Header = col.Title ?? prop.Name, Binding = new Binding(prop.Name) });
            }
            StudentsDg.ItemsSource = list;
            StudentsDg.AutoGenerateColumns = false;
            StudentsDg.Items.Refresh();
        }



        private void AddMi_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow;
            if (StudentsDg.SelectedItem != null && StudentsDg.SelectedItem is Student student)
                addStudentWindow = new AddStudentWindow(student);
            else
                addStudentWindow = new AddStudentWindow();
            //if (addStudentWindow.ShowDialog() == true)
            //    RefreshGrid();

            if (addStudentWindow.ShowDialog() == true)
            {
                Student updatedStudent = addStudentWindow.Student;

                if (StudentsDg.SelectedItem != null)
                {
                    // Update existing student in the repository
                    _studentRepository.Update(updatedStudent);
                }
                else
                {
                    // Add the new student to the repository
                    _studentRepository.Insert(updatedStudent);
                }

                // Refresh the grid after updating or adding a student
                RefreshGrid();
            }

            //var dodajStudenta = new AddStudentWindow();
            //if (dodajStudenta.ShowDialog() == true)
            //{
            //    Students.Add(dodajStudenta.Student);
            //    StudentsDg.Items.Refresh();
            //}
        }

        private void RemoveMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student studentToRemove)
            {
                _studentRepository.Delete(studentToRemove);
                RefreshGrid();
            }

            //if (StudentsDg.SelectedItem is Student)
            //{
            //    Students.Remove((Student)StudentsDg.SelectedItem);
            //    StudentsDg.Items.Refresh();
            //}
        }

        private void exitMi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
