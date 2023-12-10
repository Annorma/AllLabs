using System;
using System.Collections.Generic;
using System.Globalization;
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
using Lab_08.BLL;

namespace Lab_08_Students
{
    public partial class MainWindow : Window
    {
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

        public MainWindow()
        {
            InitializeComponent();
            Students = new List<Student>
            {
                new Student(){FirstName = "Jan", LastName="Kowalski", Faculty="WIMII", StudentNo = 111222, Grades = new List<Grade>(){
                    new Grade("PO",4.5),
                    new Grade("AM",3.5) }
                },
                new Student(){FirstName = "Michał", LastName="Nowak", Faculty="WIMII", StudentNo = 222333, Grades = new List<Grade>()
                {
                    new Grade("PO",5.0),
                    new Grade("LM",5.0) }
                }
            };
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Imię", Binding = new Binding("FirstName") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Nazwisko", Binding = new Binding("LastName") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Wydział", Binding = new Binding("Faculty") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Nr albumu", Binding = new Binding("StudentNo") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Oceny", Binding = new Binding("Grades") { Converter = new GradesConverter() } });
            StudentsDg.AutoGenerateColumns = false;
            StudentsDg.ItemsSource = Students;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var dodajStudenta = new AddStudentWindow();
            if (dodajStudenta.ShowDialog() == true)
            {
                Students.Add(dodajStudenta.Student);
                StudentsDg.Items.Refresh();
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student)
            {
                Students.Remove((Student)StudentsDg.SelectedItem);
                StudentsDg.Items.Refresh();
            }
        }
    }
}
