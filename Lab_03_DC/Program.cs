using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03_DC
{
    internal class Program
    {

        public class Car
        {
            private string model;
            private string color;
            private int liczbaDrzwi;

            protected string Model { get => model; set => model = value; }
            protected string Color { get => color; set => color = value; }
            protected int LiczbaDrzwi { get => liczbaDrzwi; set => liczbaDrzwi = value; }

            public Car()
            {
                Model = string.Empty;
                Color = string.Empty;
                LiczbaDrzwi = 0;
            }

            public Car(string model, string color, int liczbaDrzwi)
            {
                this.Model = model;
                this.Color = color;
                this.LiczbaDrzwi = liczbaDrzwi;
            }
            public virtual void Start()
            {
                Console.WriteLine("Car is riding!");
            }

            public override string ToString()
            {
                return $"Car | {Model} {Color} - {LiczbaDrzwi}";
            }

        }

        public class SportCar : Car
        {
            private double maxSpeed;

            public SportCar()
            {
                MaxSpeed = 0.0;
            }

            public SportCar(string model, string color, int liczbaDrzwi, double maxSpeed) : base(model, color, liczbaDrzwi) 
            {
                MaxSpeed = maxSpeed;
            }

            public double MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

            public override string ToString()
            {
                return $"Car | {Model} {Color} - {LiczbaDrzwi} MaxSpeed: {MaxSpeed}";
            }

            public override void Start()
            {
                Console.WriteLine("SportCar is riding!");
            }
        }
        static void Main(string[] args)
        {

        }
    }
}
