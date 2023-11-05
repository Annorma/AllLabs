using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02_Zad_01_03
{
    internal class Program
    {

        //----------============ Zad 01 ============----------↓

        public class Person
        {
            private string firstName;
            private string lastName;
            private DateTime dateOfBirth;

            public Person()
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                DateOfBirth = DateTime.MinValue;
            }
            public Person(string firstName, string lastName, DateTime dateOfBirth)
            {
                FirstName = firstName;
                LastName = lastName;
                DateOfBirth = dateOfBirth;
            }
            public string FirstName { get => firstName; set => firstName = value; }
            public string LastName { get => lastName; set => lastName = value; }
            public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

            public override string ToString()
            {
                return $"Osoba | {FirstName} {LastName} - {DateOfBirth.ToShortDateString()}";
            }

            public virtual void Details()
            {
                Console.WriteLine(ToString());
            }
        }

        public class Student : Person
        {
            private int _year;
            private int _group;
            private int _indexId;
            private List<Grade> _grades;

            public Student()
            {
                Year = 0;
                Group = 0;
                IndexId = 0;
                _grades = new List<Grade>();
            }

            public Student(string firstName, string lastName, DateTime dateOfBirth, int year, int group, int indexId) : base(firstName, lastName, dateOfBirth)
            {
                Year = year;
                Group = group;
                IndexId = indexId;
                _grades = new List<Grade>();
            }

            public int Year { get => _year; set => _year = value; }
            public int Group { get => _group; set => _group = value; }
            public int IndexId { get => _indexId; set => _indexId = value; }
            public List<Grade> Grades { get => _grades; }

            public override string ToString()
            {
                string gradeDetails = "Grades:\n";
                
                foreach (var grade in _grades)
                {
                    gradeDetails += grade.ToString() + "\n";
                
                }
                return $"Student | {FirstName} {LastName} {DateOfBirth.ToShortDateString()} | {Year} : {Group} - {IndexId}: \n{gradeDetails}";
            }

            public void AddGrade(string subjectName, double value, DateTime date)
            {
                _grades.Add(new Grade(subjectName, value, date));
            }

            public void AddGrade(Grade grade)
            {
                _grades.Add(grade);
            }

            public void DisplayGrades()
            {
                foreach (var grade in _grades)
                {
                    Console.WriteLine(grade);
                }
            }

            public void DisplayGrades(string subjectName)
            {
                string gradesInfo = string.Join("\n", _grades.Where(grade => grade.SubjectName == subjectName).Select(grade => grade.ToString()));
                Console.WriteLine(gradesInfo);
            }

            public void DeleteGrade(string subjectName, double value, DateTime date)
            {
                if (_grades != null)
                {
                    _grades.Remove(new Grade(subjectName, value, date));
                }
            }

            public void DeleteGrade(Grade grade)
            {
                _grades.Remove(grade);
            }

            public void DeleteGrades(string subjectName)
            {
                if (_grades != null)
                {
                    List<Grade> gradesToRemove = _grades.Where(grade =>
                        grade.SubjectName == subjectName).ToList();

                    foreach (var grade in gradesToRemove)
                    {
                        _grades.Remove(grade);
                    }
                }
            }

            public void DeleteGrades()
            {
                _grades.Clear();
            }
        }

        public class Player : Person
        {
            private string _position;
            private string _club;
            private int _scoredGoals;

            public Player()
            {
                Position = string.Empty;
                Club = string.Empty;
                ScoredGoals = 0;
            }

            public Player(string firstName, string lastName, DateTime dateOfBirth, string position, string club, int scoredGoals) : base(firstName, lastName, dateOfBirth)
            {
                Position = position;
                Club = club;
                ScoredGoals = scoredGoals;
            }

            public string Position { get => _position; set => _position = value; }
            public string Club { get => _club; set => _club = value; }
            public int ScoredGoals { get => _scoredGoals; set => _scoredGoals = value; }

            public override string ToString()
            {
                return $"Player | {FirstName} {LastName} {DateOfBirth.ToShortDateString()}: {Position} : {Club} - {ScoredGoals}";
            }
            public virtual void ScoreGoal()
            {
                ScoredGoals++;
            }
        }

        //----------============ Zad 01 ============----------↑


        //----------============ Zad 02 ============----------↓

        public class Grade
        {
            private string _subjectName;
            private DateTime _date;
            private double _value;

            public Grade()
            {
                SubjectName = string.Empty;
                Date = DateTime.MinValue;
                Value = 0;
            }

            public Grade(string subjectName, double value,  DateTime date)
            {
                SubjectName = subjectName;
                Date = date;
                Value = value;
            }

            public string SubjectName { get => _subjectName; set => _subjectName = value; }
            public DateTime Date { get => _date; set => _date = value; }
            public double Value { get => _value; set => _value = value; }

            public override string ToString()
            {
                return $"Grade | {SubjectName} {Date.ToShortDateString()} - {Value}";
            }

            public void Details()
            {
                Console.WriteLine(ToString());
            }
        }

        //----------============ Zad 02 ============----------↑


        //----------============ Zad 03 (domowe) ============----------↓

        public class HandballPlayer : Player
        {
            public HandballPlayer(string firstName, string lastName, DateTime dateOfBirth, string position, string club, int scoredGoals) : base(firstName, lastName, dateOfBirth, position, club, scoredGoals)
            {

            }

            public override void ScoreGoal()
            {
                base.ScoreGoal();
                Console.WriteLine("Handball player scored goal!");
            }
        }

        public class FootballPlayer : Player
        {
            public FootballPlayer(string firstName, string lastName, DateTime dateOfBirth, string position, string club, int scoredGoals) : base(firstName, lastName, dateOfBirth, position, club, scoredGoals)
            {

            }

            public override void ScoreGoal()
            {
                base.ScoreGoal();
                Console.WriteLine("Football player scored goal!");
            }
        }


        //----------============ Zad 03 (domowe) ============----------↑


        static void Main(string[] args)
        {
            //----------============ Zad 01 ============----------↓

            Person person1 = new Person("Adam", "Miś", new DateTime(1990, 3, 20, 12, 30, 10));
            Person person2 = new Student("Michał", "Kot", new DateTime(1990, 4, 13), 3, 5, 12345);
            Person person3 = new Player("Robert", "Lewandowski", new DateTime(1988, 10, 3), "Striker", "Bayern", 41);
            person1.Details();
            person2.Details();
            person3.Details();
            Student student = new Student("Krzysztof", "Jeż", new DateTime(1990, 12, 29), 2, 5, 54321);
            student.Details();
            ((Player)person3).ScoreGoal();
            person3.Details();

            //----------============ Zad 01 ============----------↑


            //----------============ Zad 02 ============----------↓

            ((Student)person2).AddGrade("PO", 5.0D, new DateTime(2011, 2, 20));
            ((Student)person2).AddGrade("Bazy Danych", 5.0D, new DateTime(2011, 2, 13));
            person2.Details();
            Grade grade = new Grade("Bazy Danych", 5.0D, new DateTime(2011, 5, 1));
            student.AddGrade(grade);
            student.AddGrade("AWWW", 5.0D, new DateTime(2011, 5, 11));
            student.AddGrade("AWWW", 4.5D, new DateTime(2011, 4, 2));
            student.Details();
            student.DeleteGrade("AWWW", 4.5D, new DateTime(2011, 4, 2));
            student.Details();
            student.DeleteGrades("AWWW");
            student.Details();
            student.AddGrade("AWWW", 5.0D, new DateTime(2011, 4, 3));
            student.DeleteGrades();
            student.Details();

            //----------============ Zad 02 ============----------↑


            //----------============ Zad 03 (domowe) ============----------↓

            Person footballPlayer =
 new FootballPlayer("Mateusz", "Żbik", new DateTime(1986, 8, 10), "striker", "FC Barcelona", 10);
            Person handballPlayer =
             new HandballPlayer("Piotr", "Kos", new DateTime(1984, 9, 14), "striker", "FC Bayern", 0);
            footballPlayer.Details();
            handballPlayer.Details();
            ((Player)handballPlayer).ScoreGoal(); // rzutowanie bezpośrednie
            (footballPlayer as Player).ScoreGoal(); // rzutowanie referencyjne
            footballPlayer.Details();
            handballPlayer.Details();

            //----------============ Zad 03 (domowe) ============----------↑

            Console.ReadKey();
        }
    }
}
