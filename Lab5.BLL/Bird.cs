using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Bird : Animal
    {
        public double Wingspan { get; set; }
        public double Endurance { get; set; }
        public Bird(string foodType, int legsCount, string origin, string species, double wingspan, double endurance) : base(foodType, legsCount, origin, species)
        {
            Wingspan = wingspan;
            Endurance = endurance;
        }

        public void Fly()
        {
            Console.WriteLine($"Bird can fly max: {Wingspan * Endurance} km");
        }
        public override string ToString()
        {
            return $"{base.ToString()} {Wingspan} {Endurance}";
        }
    }
}