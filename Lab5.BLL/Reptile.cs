using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Reptile : Animal
    {
        public bool IsPoisoned { get; set; }
        public Reptile(string foodType, int legsCount, string origin, string species, bool isPoisoned) : base(foodType, legsCount, origin, species)
        {
            IsPoisoned = isPoisoned;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {IsPoisoned}";
        }
    }
}