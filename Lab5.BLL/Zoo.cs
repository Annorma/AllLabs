using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Zoo
    {
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; } = new List<Employee>();
        public IList<Cage> Cages { get; set; } = new List<Cage>();
        public IList<Animal> Animals { get; set; } = new List<Animal>();

        public Zoo(string name, IList<Employee> employees, IList<Cage> cages, IList<Animal> animals)
        {
            Name = name;
            Employees = employees;
            Cages = cages;
            Animals = animals;
        }

        public Employee HireEmployee(string name, string surname, DateTime dateOfBirth)
        {
            Employee newEmployee = new Employee(name, surname, dateOfBirth, DateTime.Now);
            Employees.Add(newEmployee);
            return newEmployee;
        }

        public override string ToString()
        {
            return $"Zoo | {Name}";
        }
    }
}