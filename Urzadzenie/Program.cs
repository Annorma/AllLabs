using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urzadzenie
{
    internal class Program
    {
        public abstract class Urzadzenie
        {
            protected string Name { get; set; }
            protected string Description { get; set; }
            protected double Weight { get; set; }

            public Urzadzenie()
            {
                Name = string.Empty;
                Description = string.Empty;
                Weight = 0;
            }
            public Urzadzenie(string name, string description, double weight)
            {
                Name = name;
                Description = description;
                Weight = weight;
            }
            public override string ToString()
            {
                return $"{Name} - {Weight}g\nDescription: {Description}";
            }

            public abstract void Show();
        }

        public class Sluchawki : Urzadzenie
        {
            public string Model { get; set; }
            public string Color { get; set; }
            public Sluchawki()
            {
                Model = string.Empty;
                Color = string.Empty;
            }
            public Sluchawki(string name, string description, double weight, string model, string color) : base(name, description, weight)
            {
                Model = model;
                Color = color;
            }
            public override string ToString()
            {
                return $"Sluchawki | {Model} {base.ToString()}\nColor: {Color}";
            }
            public override void Show()
            {
                Console.WriteLine(ToString());
            }
        }
        static void Main(string[] args)
        {
            Sluchawki airpods = new Sluchawki("iRock", "The best music", 210.3, "Sony", "Red");
            airpods.Show();

            Console.ReadKey();
        }
    }
}
