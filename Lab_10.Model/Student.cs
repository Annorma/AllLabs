using Lab_10.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_10.Model
{
    [DbTab]
    public class Student
    {
        [DbCol, DbPrimaryKey, DisplayGrid("Nr albumu")]
        public int StudentNo { get; set; }

        [DbCol, DisplayGrid("Imię")]
        public string FirstName { get; set; }

        [DbCol, DisplayGrid("Nazwisko")]
        public string LastName { get; set; }

        [DbCol, DisplayGrid("Wydział")]
        public string Faculty { get; set; }

        [DbCol]
        public DateTime DateOfBirth { get; set; }

        [DisplayGrid("Data urodzenia")]
        public string FormattedDateOfBirth => DateOfBirth.ToShortDateString();

        [DisplayGrid("Oceny")]
        public string JoinedGrades => string.Join("; ", Grades.Select(g => $"{g.Subject}: {g.Value}"));

        public List<Grade> Grades { get; set; }
        public Student()
        {
            Grades = new List<Grade>();
        }

        public bool HasGrades()
        {
            return Grades != null && Grades.Any();
        }
    }
}
