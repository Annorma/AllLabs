using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Mammal : Animal
    {
        public string Country { get; set; }
        public Mammal(string foodType, int legsCount, string origin, string species, string country) : base(foodType, legsCount, origin, species)
        {
            Country = country;
        }
        public override string ToString()
        {
            return $"{base.ToString()} {Country}";
        }
    }
}