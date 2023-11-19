using Generic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Employee : IDisplayable, IContainer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public Employee(string firstName, string lastName, DateTime dateOfBirth, DateTime hireDate)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            HireDate = hireDate;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {DateOfBirth.ToShortDateString()} {HireDate.ToShortDateString()}";
        }
    }
}