using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for AddGradeWindow.xaml
    /// </summary>
    public partial class AddGradeWindow : Window
    {
        private readonly Student selectedStudent;

        public AddGradeWindow(Student student)
        {
            InitializeComponent();
            selectedStudent = student;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(SubjectTb.Text, @"^[A-Za-z0-9\s]+$") ||
                    !Regex.IsMatch(ValueTb.Text, @"^(?:\d+(?:\.\d*)?|\.\d+)$"))
                {
                    MessageBox.Show("Wprowadzone dane są niepoprawne.");
                    return;
                }

                string subject = SubjectTb.Text;
                double value = double.Parse(ValueTb.Text, CultureInfo.InvariantCulture);

                Grade newGrade = new Grade
                {
                    Date = DateTime.Now,
                    Subject = subject,
                    Value = value,
                    StudentNo = selectedStudent.StudentNo
                };

                using (StudentsDbContext dbContext = new StudentsDbContext())
                {
                    dbContext.Grades.Add(newGrade);
                    dbContext.SaveChanges();
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd dodawania oceny: {ex.Message}\n\nInner Exception: {ex.InnerException?.Message}");
            }
        }
    }
}
