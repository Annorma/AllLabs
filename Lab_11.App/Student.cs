using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_11.App
{
    public class Student
    {
        [Key]
        public int StudentNo { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Faculty { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FormattedDateOfBirth => DateOfBirth.ToShortDateString();

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
