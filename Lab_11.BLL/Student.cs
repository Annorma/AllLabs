using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab_11.BLL
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }

        public int StudentNo { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public Student()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Faculty = string.Empty;
            StudentNo = 0;
        }

        public Student(string firstName, string lastName, string faculty, int studentNo, List<Grade> grades)
        {
            FirstName = firstName;
            LastName = lastName;
            Faculty = faculty;
            StudentNo = studentNo;
            Grades = grades;
        }
    }
}
