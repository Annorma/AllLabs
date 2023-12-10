using System;
using System.Collections.Generic;

namespace Lab_08.BLL
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public int StudentNo { get; set; }
        public IList<Grade> Grades { get; set; } = new List<Grade>();

        public Student()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Faculty = string.Empty;
            StudentNo = 0;
        }

        public Student(string firstName, string lastName, string faculty, int studentNo, IList<Grade> grades)
        {
            FirstName = firstName;
            LastName = lastName;
            Faculty = faculty;
            StudentNo = studentNo;
            Grades = grades;
        }
    }
}
