using Lab_08.BLL;
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

namespace Lab_08_Students
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow(Student? student = null)
        {
            InitializeComponent();

            if (student != null)
            {
                Student = student;
                FirstNameTb.Text = student.FirstName;
                LastNameTb.Text = student.LastName;
                FacultyTb.Text = student.Faculty;
                StudentNoTb.Text = student.StudentNo.ToString();
                GradesTb.Text = student.Grades.ToString();
            }
            else
            {
                Student = new Student();
                Student.Grades = new List<Grade>();
            };

        }
        public Student Student { get; set; }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (
                 !Regex.IsMatch(FirstNameTb.Text, @"^\p{Lu}\p{Ll}{1,20}$") ||
                 !Regex.IsMatch(LastNameTb.Text, @"^\p{Lu}\p{Ll}{1,20}$") ||
                 !Regex.IsMatch(StudentNoTb.Text, @"^[0-9]{4,10}$") ||
                 !Regex.IsMatch(FacultyTb.Text, @"^[\p{Lu}|\p{Ll}]{1,12}$") ||
                 !Regex.IsMatch(GradesTb.Text, @"^[A-Za-z]+\s*:\s*\d+(\.\d+)?(?:\s*,\s*[A-Za-z]+\s*:\s*\d+(\.\d+)?)*$")
                 )
                {
                    MessageBox.Show("Wprowadzone dane są niepoprawne.");
                    return;
                };
            Student.FirstName = FirstNameTb.Text;
            Student.LastName = LastNameTb.Text;
            Student.StudentNo = int.Parse(StudentNoTb.Text);
            Student.Faculty = FacultyTb.Text;

            List<Grade> grades = ParseGrades(GradesTb.Text);
            Student.Grades = grades;
            DialogResult = true;
        }

        private List<Grade> ParseGrades(string gradesText)
        {
            var grades = new List<Grade>();

            var gradeStrings = gradesText.Split(", ");
            foreach (var gradeString in gradeStrings)
            {
                var parts = gradeString.Split(": ");
                if (parts.Length == 2)
                {
                    var subject = parts[0];
                    if (double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var gradeValue))
                    {
                        grades.Add(new Grade { Subject = subject, Value = gradeValue });
                    }
                }
            }

            return grades;
        }
    }
}
