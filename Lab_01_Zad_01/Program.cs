using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_01_Zad_01_03
{
    //----------============ Zad 01 ============----------↓
    public class Car
    {
        private static int _carCount = 0;
        private string _brand;
        private string _model;
        private int _doorCount;
        private float _engineVolume;
        private double _avgConsump;
        private string _registrationNumber;

        public Car()
        {
            _brand = "none";
            _model = "none";
            _doorCount = 0;
            _engineVolume = 0;
            _avgConsump = 0;
            _registrationNumber = "none";
            _carCount++;
        }
        public Car(string brand, string model, int doorCount, float engineVolume, double avgConsump, string registrationNumber)
        {
            _brand = brand;
            _model = model;
            _doorCount = doorCount;
            _engineVolume = engineVolume;
            _avgConsump = avgConsump;
            _registrationNumber = registrationNumber;
            _carCount++;
        }

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int DoorCount
        {
            get { return _doorCount; }
            set { _doorCount = value; }
        }
        public float EngineVolume
        {
            get { return _engineVolume; }
            set { _engineVolume = value; }
        }
        public double AvgConsump
        {
            get { return _avgConsump; }
            set { _avgConsump = value; }
        }
        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public double CalculateConsump(double roadLenght)
        {
            return (roadLenght / 100) * _avgConsump;
        }
        public double CalculateCost(double roadLenght, double petrolCost)
        {
            return CalculateConsump(roadLenght) * petrolCost;
        }

        public override string ToString()
        {
            return $"Car | Brand: {Brand}, Model: {Model}, NumOfDoors: {DoorCount}, EngineVol: {EngineVolume}, AvgConsump:{AvgConsump}";
        }
        public void Details()
        {
            Console.WriteLine(ToString());
        }

        public static void DisplayCarCount()
        {
            Console.WriteLine($"Liczba samochodów: {_carCount}");
        }
    }

    //----------============ Zad 01 ============----------↑


    //----------============ Zad 02 ============----------↓


    public class Garage
    {
        private Car[] _cars;
        private string _address;
        private int _carsCount;
        private int _capacity;

        public Garage()
        {
            _address = "none";
            _carsCount = 0;
            _capacity = 0;
        }
        public Garage(string address, int capacity)
        {
            _address = address;
            _capacity = capacity;
            _cars = new Car[capacity];
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public int CarsCount
        {
            get { return _carsCount; }
            set { _carsCount = value; }
        }
        
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                _cars = new Car[value];
            }
        }

        public void CarIn(Car car)
        {
            if (Capacity > 0 && CarsCount < Capacity)
            {
                _cars[CarsCount] = car;
                CarsCount++;
            }
            else { Console.WriteLine("Garage is full!"); }
        }

        public Car CarOut()
        {
            if (CarsCount > 0)
            {
                Car carToReturn = _cars[CarsCount - 1];
                _cars[CarsCount - 1] = null;
                CarsCount--;
                return carToReturn;
            }
            else
            {
                Console.WriteLine("Garage is empty! You can't take out a car.");
                return null;
            }
        }

        public override string ToString()
        {
            string carsInfo = string.Join("\n", _cars.Where(car => car != null).Select(car => car.ToString()));
            return $"Garage Address: {Address}\nCapacity: {Capacity}\nCars Count: {CarsCount}\nCars:\n{carsInfo}";
        }


        public void Details()
        {
            Console.WriteLine(ToString());
        }
    }


    //----------============ Zad 02 ============----------↑


    //----------============ Zad 03 (domowe) ============----------↓

    public class Person
    {
        private string[] _registrationNumbers;
        private string _firstName;
        private string _lastName;
        private string _address;
        private int _carsCount;

        public Person()
        {
            _registrationNumbers = new string[3];
            _firstName = "none";
            _lastName = "none";
            _address = "none";
            _carsCount = 0;
        }

        public Person(string firstName, string lastName, string address)
        {
            _registrationNumbers = new string[3];
            _firstName = firstName;
            _lastName = lastName;
            _address = address;
        }

        public Person(string firstName, string lastName, string address, Car[] cars)
        {
            _firstName = firstName;
            _lastName = lastName;
            _address = address;

            if (cars.Length > 0 && cars.Length <= 3 )
            {
                _registrationNumbers = new string[3];
                for (int i = 0; i < cars.Length; i++)
                {
                    if (cars[i] != null)
                    {
                       _registrationNumbers[i] = cars[i].RegistrationNumber;
                        CarsCount++;
                    }
                }
            }
            else { Console.WriteLine("Person can have only 3 cars!"); }

        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string Address
        {
            get { return _address; }
            set {  _address = value; }
        }
        public int CarsCount
        {
            get { return _carsCount; }
            set { _carsCount = value; }
        }

        public void AddCarRegistrationNumber(string registrationNumber)
        {
            if (CarsCount >= 0 && CarsCount < 3)
            {
                _registrationNumbers[CarsCount] = registrationNumber;
                CarsCount++;
            }
            else { Console.WriteLine("You can't have more than 3 cars!"); }
        }

        public void RemoveCarRegistrationNumber(string registrationNumber)
        {
            if (CarsCount > 0)
            {
                for (int i = 0; i < CarsCount; i++)
                {
                    if (_registrationNumbers[i] == registrationNumber)
                    {
                        _registrationNumbers[i] = null;
                        CarsCount--;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("You don't have any cars to remove.");
            }
        }

        public override string ToString()
        {
            if (_registrationNumbers != null)
            {
                string carInfo = string.Join("\n", _registrationNumbers.Where(regNum => !string.IsNullOrEmpty(regNum)));
                return $"Person | Name: {FirstName}, Surname: {LastName}, Address: {Address}, Cars Count: {CarsCount}\nCar Registration Numbers:\n{carInfo}";
            }
            else { return $"Person | Name: {FirstName}, Surname: {LastName}, Address: {Address}, Cars Count: {CarsCount}\nCar Registration Numbers:\nnone"; }
        }

        public void Details()
        {
            Console.WriteLine(ToString());
        }

    }

    //----------============ Zad 03 (domowe) ============----------↑

    internal class Program
    {

        static void Main(string[] args)
        {
            //----------============ Zad 01 ============----------↓
            
            Car car1 = new Car();
            car1.Details();
            car1.Brand = "Fiat";
            car1.Model = "126p";
            car1.DoorCount = 2;
            car1.EngineVolume = 650;
            car1.AvgConsump = 6.0;
            car1.RegistrationNumber = "KR12345";
            car1.Details();
            Car car2 = new Car("Syrena", "105", 2, 0.8f, 7.6d, "WE1234");
            car2.Details();
            Console.WriteLine(car1);
            double routeConsumption = car2.CalculateConsump(500);
            Console.WriteLine($"Route consumption: {routeConsumption} l");
            double routeCost = car2.CalculateCost(500, 5);
            Console.WriteLine($"Route cost: {routeCost}");
            Car.DisplayCarCount();
            Console.WriteLine("\r\n=========================================\r\n");
            
            //----------============ Zad 01 ============----------↑


            //----------============ Zad 02 ============----------↓

            Garage garage1 = new Garage();
            garage1.Address = "ul. Garażowa 1";
            garage1.Capacity = 1;
            Garage garage2 = new Garage("ul. Garażowa 2", 2);
            garage1.CarIn(car1);
            garage1.Details();
            garage1.CarIn(car2);
            garage2.CarIn(car2);
            var movedCar = garage1.CarOut();
            garage2.CarIn(movedCar);
            garage2.Details();
            garage1.Details();
            garage2.CarOut();
            garage2.Details();
            garage2.CarOut();
            garage2.CarOut();
            garage2.Details();
            garage1.Details();
            Console.WriteLine("\r\n=========================================\r\n");

            //----------============ Zad 02 ============----------↑


            //----------============ Zad 03 (domowe) ============----------↓

            Person person1 = new Person();
            person1.FirstName = "John";
            person1.LastName = "Snow";
            person1.Address = "Dragon St.";
            person1.Details();
            person1.AddCarRegistrationNumber("BK 0001 AH");
            person1.Details();
            person1.RemoveCarRegistrationNumber("BK 0001 AH");
            person1.Details();

            Car[] cars = new Car[3];
            cars[0] = car1;
            cars[1] = car2;
            Person person2 = new Person("Mike", "Wazowski", "Monster St.", cars);
            person2.Details();

            Console.WriteLine("\r\n=========================================\r\n");

            //----------============ Zad 03 (domowe) ============----------↑

            Console.ReadKey();
        }
    }
}

