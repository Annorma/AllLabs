using System;
using Generic.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Zoo : IDisplayable, IContainer
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

        public CageSupervisor HireEmployee(string name, string surname, DateTime dateOfBirth)
        {
            CageSupervisor newEmployee = new CageSupervisor(name, surname, dateOfBirth, DateTime.Now, new List<Cage>());
            Employees.Add(newEmployee);
            return newEmployee;
        }

        public Cage BuildCage(int capacity, bool isClear)
        {
            Cage newCage = new Cage(capacity, isClear, new List<Animal>());
            Cages.Add(newCage);
            return newCage;
        }
        public void ExpandCage(Cage cage, int capacity)
        {
            Cages.FirstOrDefault(cage).Capacity += capacity;
        }


        public override string ToString()
        {
            string employees = "\nEmployees:\n";
            foreach (var employee in Employees)
            {
                employees += employee.ToString() + "\n";
            }

            string cages = "\nCages:\n";
            foreach (var cage in Cages)
            {
                cages += cage.ToString() + "\n";
            }

            string animals = "\nAnimals:\n";
            foreach (var animal in Animals)
            {
                animals += animal.ToString() + "\n";
            }
            return $"Zoo | {Name} {employees} {cages} {animals}";
        }
    }
}