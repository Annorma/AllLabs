using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class CageSupervisor : Employee
    {
        public IList<Cage> Cages { get; set; }
        public CageSupervisor(string name, string surname, DateTime dateOfBirth, DateTime hireDate, IList<Cage> cages) : base(name, surname, dateOfBirth, hireDate)
        {
            Cages = cages;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}