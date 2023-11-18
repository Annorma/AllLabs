using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public Employee(string name, string surname, DateTime dateOfBirth, DateTime hireDate)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            HireDate = hireDate;
        }
        public override string ToString()
        {
            return $"{Name} {Surname} {DateOfBirth.ToShortDateString()} {HireDate.ToShortDateString()}";
        }
    }
}