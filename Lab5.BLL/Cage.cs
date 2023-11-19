using System;
using System.Collections.Generic;
using Generic.Extensions;
using System.Linq;
using System.Text;

namespace Lab5.BLL
{
    public class Cage : IDisplayable, IContainer
    {
        public int Capacity { get; set; }
        public bool IsClear { get; set; }
        public IList<Animal> Animals { get; set; } = new List<Animal>();

        public Cage(int capacity, bool isClear, IList<Animal> animals)
        {
            Capacity = capacity;
            IsClear = isClear;
            Animals = animals;
        }

        public override string ToString()
        {
            string animals = "\nAnimals:\n";

            foreach (var animal in Animals)
            {
                animals += animal.ToString() + "\n";
            }
            return $"Cage | {Capacity} {IsClear}{animals}";
        }
    }
}