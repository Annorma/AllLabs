using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kolokwium_01
{
    public interface IDisplayable
    {
        string ToString();
    }

    public static class DisplayActionExtensions
    {
        public static void Print<T>(this T obj)
        {
            Console.WriteLine(obj != null ? obj.ToString() : "null");
        }

        public static void Print<T>(this IList<T> list)
        {
            foreach (var item in list)
            {
                item.Print();
            }
        }
    }

    internal class Program
    {
        public abstract class Person : IDisplayable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Country { get; set; }
            public Person()
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                DateOfBirth = DateTime.MinValue;
                Country = string.Empty;
            }
            public Person(string FirstName, string LastName, DateTime DateOfBirth, string Country)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.DateOfBirth = DateOfBirth;
                this.Country = Country;
            }
            public override string ToString()
            {
                return $"{FirstName} {LastName} {DateOfBirth.ToShortDateString()} {Country}";
            }
            public abstract void StartJob();
        }

        public class Patient : Person
        {
            public string BloodType { get; set; }
            public Patient(string firstName, string lastName, DateTime dateOfBirth, string country, string bloodType) : base(firstName, lastName, dateOfBirth, country)
            {
                BloodType = bloodType;
            }

            public override string ToString()
            {
                return $"{base.ToString()} {BloodType}";
            }
            public override void StartJob()
            {
                Console.WriteLine("Patient started a job!");
            }

        }

        public class Analyst : Person
        {
            public int Staz { get; set; }
            public Analyst(string firstName, string lastName, DateTime dateOfBirth, string country, int staz) : base(firstName, lastName, dateOfBirth, country)
            {
                Staz = staz;
            }

            public override string ToString()
            {
                return $"Analyst | {base.ToString()} {Staz}";
            }
            public override void StartJob()
            {
                Console.WriteLine("Analyst started a job!");
            }
        }

        public class TestResult : IDisplayable
        {
            public int NumberOfRedBloodCells { get; set; }
            public int NumberOfWhiteBloodCells { get; set; }

            public TestResult(int numberOfRedBloodCells, int numberOfWhiteBloodCells)
            {
                NumberOfRedBloodCells = numberOfRedBloodCells;
                NumberOfWhiteBloodCells = numberOfWhiteBloodCells;
            }

            public override string ToString()
            {
                return $"TestResult | RedBloodCells: {NumberOfRedBloodCells} WhiteBloodCells: {NumberOfWhiteBloodCells}";
            }
        }

        public class BloodTest : IDisplayable
        {
            private static int testCount;
            public string Name { get; set; }
            public double Price { get; set; }
            public IList<Analyst> Analysts { get; set; } = new List<Analyst>();
            public IList<TestResult> Results { get; set; } = new List<TestResult>();
            public Patient Patient { get; set; }

            public static int TestCount { get { return testCount; } }

            static BloodTest()
            {
                testCount = 0;
            }

            public BloodTest(string name, double price, IList<Analyst> analysts, IList<TestResult> results, Patient patient)
            {
                Name = name;
                Price = price;
                Analysts = analysts;
                Results = results;
                Patient = patient;
                testCount++;
            }

            public override string ToString()
            {
                string analysts = string.Empty;
                foreach (var analyst in Analysts)
                {
                    analysts += analyst.ToString() + "\n";
                }
                string results = string.Empty;
                foreach (var result in Results)
                {
                    results += result.ToString() + "\n";
                }

                return $"BloodTest | {Name} - {Price} zł. Patient: {Patient}\nAnalysts:\n{analysts}\nResults: {results}";
            }
        }

        public class Infirmary : IDisplayable // Ambulatorium
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public IList<BloodTest> BloodTests { get; set; } = new List<BloodTest>();

            public Infirmary(string name, string address, IList<BloodTest> bloodTests)
            {
                Name = name;
                Address = address;
                BloodTests = bloodTests;
            }

            public void FindTestsByName(string name)
            {
                Console.WriteLine("----==== FindTestsByName ====----");
                foreach (var test in BloodTests)
                {
                    if (test.Name == name)
                    {
                        Console.WriteLine("");
                    }
                }
            }

            public BloodTest? FindTestByName(string name)
            {
                return BloodTests.FirstOrDefault(bt => bt.Name == name);
            }

            public double CalculateAvgPrice(int age)
            {
                double avgPrice = 0;
                foreach (var test in BloodTests)
                {
                    if (DateTime.Now.Year - test.Patient.DateOfBirth.Year == age)
                    {
                        avgPrice += test.Price;
                    }
                }
                return avgPrice / BloodTests.Count;
            }

            public override string ToString()
            {
                string bloodTests = string.Empty;
                foreach (var bloodTest in BloodTests)
                {
                    bloodTests += bloodTest.ToString() + "\n";
                }
                return $"Infirmary | {Name} - {Address}\nBloodTests:\n{bloodTests}";
            }
        }


        static void Main(string[] args)
        {
            Patient p1 = new Patient("Jan", "Kowalski", new DateTime(2000, 2, 15), "Poland", "B+");
            Patient p2 = new Patient("John", "Snow", new DateTime(1980, 11, 1), "USA", "A+");
            Patient p3 = new Patient("Anna", "de Armas", new DateTime(1995, 6, 20), "Spain", "B-");

            Analyst a1 = new Analyst("Bartosz", "Nowak", new DateTime(1965, 6, 3), "Poland", 10);
            Analyst a2 = new Analyst("Artem", "Delidon", new DateTime(1993, 3, 10), "Ukraine", 5);

            TestResult tr1 = new TestResult(10, 15);
            TestResult tr2 = new TestResult(100, 500);

            var analystList = new List<Analyst> { a1, a2 };
            var resultsList = new List<TestResult> { tr1, tr2 };

            BloodTest bt1 = new BloodTest("Test1", 1500.30, analystList, resultsList, p1);

            var bloodTestsList = new List<BloodTest> { bt1 };


            Infirmary i1 = new Infirmary("Ambulatorium 1", "Jana Długosza St.", bloodTestsList);

            i1.Print();

        }
    }
}