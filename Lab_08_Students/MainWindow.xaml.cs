using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Xml.Serialization;
using Lab_08.BLL;
using Microsoft.Win32;

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
                new Student(){FirstName = "Jan", LastName="Kowalski", Faculty="WIMII", StudentNo = 116843, Grades = new List<Grade>(){
                    new Grade("PO",4.5),
                    new Grade("AM",3.5) }
                },
                new Student(){FirstName = "Michał", LastName="Nowak", Faculty="WIMII", StudentNo = 468735, Grades = new List<Grade>()
                {
                    new Grade("PO",4.5),
                    new Grade("LM",5.0) }
                },
                new Student(){FirstName = "Marcin", LastName="Jakubski", Faculty="WIP", StudentNo = 647674, Grades = new List<Grade>()
                {
                    new Grade("GH",2.0),
                    new Grade("WF",5.0) }
                }
            };
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Imię", Binding = new Binding("FirstName") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Nazwisko", Binding = new Binding("LastName") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Wydział", Binding = new Binding("Faculty") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header="Nr albumu", Binding = new Binding("StudentNo") });
            StudentsDg.Columns.Add(new DataGridTextColumn() { Header = "Oceny", Binding = new Binding("Grades") { Converter = new GradesConverter() } });
            StudentsDg.AutoGenerateColumns = false;
            StudentsDg.ItemsSource = Students;
            StudentsDg.IsReadOnly = true;
        }

        private void AddMi_Click(object sender, RoutedEventArgs e)
        {
            var dodajStudenta = new AddStudentWindow();
            if (dodajStudenta.ShowDialog() == true)
            {
                Students.Add(dodajStudenta.Student);
                StudentsDg.Items.Refresh();
            }
        }

        private void RemoveMi_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDg.SelectedItem is Student)
            {
                Students.Remove((Student)StudentsDg.SelectedItem);
                StudentsDg.Items.Refresh();
            }
        }

        private void SaveTxtMi_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Zapisz do pliku TXT"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string txtFilePath = saveFileDialog.FileName;

                using (StreamWriter sw = new StreamWriter(txtFilePath))
                {
                    foreach (var student in Students)
                    {
                        sw.WriteLine("[[Student]]");
                        sw.WriteLine("[FirstName]");
                        sw.WriteLine(student.FirstName);
                        sw.WriteLine("[LastName]");
                        sw.WriteLine(student.LastName);
                        sw.WriteLine("[StudentNo]");
                        sw.WriteLine(student.StudentNo);
                        sw.WriteLine("[Faculty]");
                        sw.WriteLine(student.Faculty);

                        if (student.Grades != null)
                        {
                            foreach (var grade in student.Grades)
                            {
                                sw.WriteLine("[[Grade]]");
                                sw.WriteLine("[Subject]");
                                sw.WriteLine(grade.Subject);
                                sw.WriteLine("[Value]");
                                sw.WriteLine(grade.Value);
                                sw.WriteLine("[[]]");
                            }
                        }
                        sw.WriteLine("[[]]");
                    }
                }
                MessageBox.Show("Dane zostały zapisane do pliku txt.");
            }
            else
            {
                MessageBox.Show("Zapisywanie anulowane przez użytkownika.");
            }
        }

        private void LoadTxtMi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Otwórz plik TXT"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string txtFilePath = openFileDialog.FileName;

                Students = new List<Student>();
                string[] lines = File.ReadAllLines(txtFilePath);
                Student? currentStudent = null;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("[[Student]]"))
                    {
                        currentStudent = new Student();
                        i++; // Next line

                        while (i < lines.Length && !lines[i].StartsWith("[[]]"))
                        {
                            if (lines[i].StartsWith("[FirstName]"))
                            {
                                currentStudent.FirstName = lines[++i];
                            }
                            else if (lines[i].StartsWith("[LastName]"))
                            {
                                currentStudent.LastName = lines[++i];
                            }
                            else if (lines[i].StartsWith("[StudentNo]"))
                            {
                                int.TryParse(lines[++i], out int studentNo);
                                currentStudent.StudentNo = studentNo;
                            }
                            else if (lines[i].StartsWith("[Faculty]"))
                            {
                                currentStudent.Faculty = lines[++i];
                            }
                            else if (lines[i].StartsWith("[[Grade]]"))
                            {
                                i++; // Next line
                                var grade = new Grade();

                                while (i < lines.Length && !lines[i].StartsWith("[[]]"))
                                {
                                    if (lines[i].StartsWith("[Subject]"))
                                    {
                                        grade.Subject = lines[++i];
                                    }
                                    else if (lines[i].StartsWith("[Value]"))
                                    {
                                        double.TryParse(lines[++i], out double value);
                                        grade.Value = value;
                                    }
                                    i++; // Next line
                                }
                                if (currentStudent.Grades == null)
                                {
                                    currentStudent.Grades = new List<Grade>();
                                }
                                currentStudent.Grades.Add(grade);
                            }
                            i++; // Next line
                        }
                        Students.Add(currentStudent);
                    }
                }
                StudentsDg.ItemsSource = Students;
                MessageBox.Show("Dane zostały wczytane z pliku txt.");
            }
            else
            {
                MessageBox.Show("Wczytanie anulowane przez użytkownika.");
            }
        }

        private void SaveXmlMi_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                Title = "Zapisz do pliku XML"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    xmlSerializer.Serialize(sw, Students);
                }

                MessageBox.Show("Dane zostały zapisane do pliku XML.");
            }
            else
            {
                MessageBox.Show("Zapisywanie anulowane przez użytkownika.");
            }
        }

        private void LoadXmlMi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                Title = "Otwórz plik XML"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    Students = (List<Student>)xmlSerializer.Deserialize(sr);
                }

                StudentsDg.ItemsSource = Students;
                MessageBox.Show("Dane zostały wczytane z pliku XML.");
            }
            else
            {
                MessageBox.Show("Wczytanie anulowane przez użytkownika.");
            }
        }

        private void SaveJsonMi_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Zapisz do pliku JSON";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Serialize the list to JSON
                string jsonStr = JsonSerializer.Serialize(Students, new JsonSerializerOptions { WriteIndented = true });

                // Save the JSON to the selected file
                File.WriteAllText(saveFileDialog.FileName, jsonStr);

                Console.WriteLine("Dane zostały zapisane do pliku JSON.");
            }
            else
            {
                Console.WriteLine("Zapisywanie anulowane przez użytkownika.");
            }
        }

        private void LoadJsonMi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Otwórz plik JSON"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string jsonStr = File.ReadAllText(openFileDialog.FileName);
                Students = JsonSerializer.Deserialize<List<Student>>(jsonStr);
                StudentsDg.ItemsSource = Students;
                MessageBox.Show("Dane zostały wczytane z pliku JSON.");
            }
            else
            {
                MessageBox.Show("Wczytanie anulowane przez użytkownika.");
            }
        }

        private void exitMi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
