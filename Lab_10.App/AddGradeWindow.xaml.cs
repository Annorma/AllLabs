using Lab_10.DAL;
using Lab_10.Model;
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

namespace Lab_10.App
{
    /// <summary>
    /// Interaction logic for AddGradeWindow.xaml
    /// </summary>
    public partial class AddGradeWindow : Window
    {
        private readonly Student selectedStudent;
        private readonly IRepository<Grade> _gradeRepository;

        public AddGradeWindow(Student student, IRepository<Grade> gradeRepository)
        {
            InitializeComponent();
            selectedStudent = student;
            _gradeRepository = gradeRepository;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                 !Regex.IsMatch(SubjectTb.Text, @"^[A-Za-z0-9\s]+$") ||
                 !Regex.IsMatch(ValueTb.Text, @"^(?:\d+(?:\.\d*)?|\.\d+)$")
                 )
                {
                    MessageBox.Show("Wprowadzone dane są niepoprawne.");
                    return;
                };
                string subject = SubjectTb.Text;
                double value = double.Parse(ValueTb.Text, CultureInfo.InvariantCulture);

                Grade newGrade = new Grade
                {
                    Date = DateTime.Now,
                    Subject = subject,
                    Value = value,
                    StudentNo = selectedStudent.StudentNo
                };

                selectedStudent.Grades.Add(newGrade);
                _gradeRepository.Insert(newGrade);

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd dodawania oceny: {ex.Message}");
            }
        }
    }
}
