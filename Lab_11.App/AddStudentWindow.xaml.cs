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

namespace Lab_11.App
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public Student Student { get; set; }

        public AddStudentWindow(Student student = null)
        {
            InitializeComponent();

            if (student != null)
            {
                Student = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Faculty = student.Faculty,
                    StudentNo = student.StudentNo,
                    Grades = new List<Grade>(student.Grades),
                    DateOfBirth = student.DateOfBirth
                };

                FirstNameTb.Text = Student.FirstName;
                LastNameTb.Text = Student.LastName;
                FacultyTb.Text = Student.Faculty;
                StudentNoTb.Text = Student.StudentNo.ToString();
                DatePck.SelectedDate = Student.DateOfBirth;
            }
            else
            {
                Student = new Student();
                Student.Grades = new List<Grade>();
            };

        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (
                 !Regex.IsMatch(FirstNameTb.Text, @"^\p{Lu}\p{Ll}{1,20}$") ||
                 !Regex.IsMatch(LastNameTb.Text, @"^\p{Lu}\p{Ll}{1,20}$") ||
                 !Regex.IsMatch(StudentNoTb.Text, @"^[0-9]{4,10}$") ||
                 !Regex.IsMatch(FacultyTb.Text, @"^[\p{Lu}|\p{Ll}]{1,12}$") ||
                 DatePck.SelectedDate > DateTime.Now
                 )
            {
                MessageBox.Show("Wprowadzone dane są niepoprawne.");
                return;
            };
            Student.FirstName = FirstNameTb.Text;
            Student.LastName = LastNameTb.Text;
            Student.StudentNo = int.Parse(StudentNoTb.Text);
            Student.Faculty = FacultyTb.Text;
            Student.DateOfBirth = DatePck.SelectedDate.Value;

            using (StudentsDbContext dbContext = new StudentsDbContext())
            {
                dbContext.Students.Add(Student);
                dbContext.SaveChanges();
            }

            DialogResult = true;
        }
    }
}
