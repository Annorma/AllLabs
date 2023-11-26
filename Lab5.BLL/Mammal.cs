using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Mammal : Animal
    {
        public string Environment { get; set; }
        public Mammal(string foodType, int legsCount, string origin, string species, string country) : base(foodType, legsCount, origin, species)
        {
            Environment = country;
        }
        public override string ToString()
        {
            return $"{base.ToString()} {Environment}";
        }
    }
}