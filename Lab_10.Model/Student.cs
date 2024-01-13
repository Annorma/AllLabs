using System;
using System.Collections.Generic;

namespace Lab_10.Model
{
    [DbTab]
    public class Student
    {
        [DbCol]
        [DbPrimaryKey]
        [DisplayGrid]
        public int StudentNo { get; set; }
        [DbCol]
        [DisplayGrid]
        public string FirstName { get; set; }
        [DbCol]
        [DisplayGrid]
        public string Lastame { get; set; }
        [DbCol]
        [DisplayGrid]
        public string Faculty { get; set; }
        [DbCol]
        [DisplayGrid]
        public DateTime DateOfBirth { get; set; }
        public List<Grade> Grades { get; set; }
        [DisplayGrid("Grades")]
        public string JoinedGrades => string.Join(", ", Grades);
        public Student()
        {
            Grades = new List<Grade>();
        }
    }

}
