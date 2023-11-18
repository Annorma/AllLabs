using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04_Zad_01_02_New
{
    //----------============ Zad 02 ============----------↓

    public interface IDisplayable // Interfejs dla obiektów, które mogą być wyświetlane jako ciągi znaków
    {
        string ToString();
    }

    public static class DisplayActionExtensions // Metody rozszerzające do wyświetlania obiektów
    {
        public static void Print<T>(this T obj) // Wyświetlanie obiektu przy użyciu Console.WriteLine
        {
            Console.WriteLine(obj != null ? obj.ToString() : "null");
        }

        public static void Print<T>(this IList<T> list) // Wyświetlanie listy obiektów przy użyciu metody rozszerzającej Print
        {
            foreach (var item in list)
            {
                item.Print();
            }
        }
    }

    public interface IContainer { } // Interfejs dla obiektów kontenerowych

    public static class CrudActionExtensions // Metody rozszerzające do operacji CRUD (Create, Read, Update, Delete) na obiektach kontenerowych
    {
        public static IList<T> Set<T>(this IContainer containerObject) // Pobiera listę z obiektu kontenerowego na podstawie właściwości
        {
            var collectionProperty = containerObject.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, zwracamy tę listę
                return (IList<T>)collectionProperty.GetValue(containerObject);
            }

            // Jeśli nie znaleziono właściwości, zwracamy null
            return null;
        }

        public static void ForEach<T>(this IList<T> list, Action<T> action)  // Wykonuje akcję dla każdego elementu na liście
        {
            var collectionProperty = list.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy lista posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(list); // Jeśli tak, uzyskujemy dostęp do listy

                foreach (var item in collection) // Wykonujemy podaną akcję dla każdego elementu na liście
                {
                    action(item);
                }
            }
        }

        public static T Get<T>(this IContainer container, Func<T, bool>? searchPredicate = null) // Pobranie elementu z obiektu kontenerowego na podstawie predykatu wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy

                // Jeśli podano predykat wyszukiwania, zwracamy pierwszy pasujący element, w przeciwnym razie pierwszy element listy
                if (searchPredicate != null)
                {
                    return collection.FirstOrDefault(searchPredicate); 
                }
                else
                {
                    return collection.FirstOrDefault();
                }
            }

            return default(T); // Jeśli nie znaleziono właściwości lub listy, zwracamy domyślną wartość dla typu T
        }

        public static IList<T> GetList<T>(this IContainer container, Func<T, bool>? searchPredicate = null) // Pobranie listy elementów z obiektu kontenerowego na podstawie predykatu wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy

                // Jeśli podano predykat wyszukiwania, zwracamy listę pasujących elementów, w przeciwnym razie całą listę
                if (searchPredicate != null)
                {
                    return collection.Where(searchPredicate).ToList();
                }
                else
                {
                    return collection.ToList();
                }
            }

            // Jeśli nie znaleziono właściwości lub listy, zwracamy nową pustą listę dla typu T
            return new List<T>();
        }

        public static T Add<T>(this IContainer container, T obj) // Dodanie elementu do obiektu kontenerowego
        {
            var collectionProperty = obj.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, uzyskujemy dostęp do listy i dodajemy do niej nowy element
                var collection = (IList<T>)collectionProperty.GetValue(obj);
                collection.Add(obj);
            }

            return obj; // Zwracamy dodany obiekt
        }

        public static bool Remove<T>(this IContainer container, Func<T, bool> searchFn) // Usunięcie elementów z obiektu kontenerowego na podstawie funkcji wyszukiwania
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                var collection = (IList<T>)collectionProperty.GetValue(container); // Jeśli tak, uzyskujemy dostęp do listy
                var itemsToRemove = collection.Where(searchFn).ToList(); // Znajdujemy elementy do usunięcia na podstawie funkcji wyszukiwania

                // Usuwamy znalezione elementy z listy
                foreach (var item in itemsToRemove)
                {
                    collection.Remove(item);
                }

                // Zwracamy informację, czy cokolwiek zostało usunięte
                return itemsToRemove.Count > 0;
            }

            // Jeśli nie znaleziono właściwości lub listy, zwracamy false
            return false;
        }

        public static IContainer AddRange<T>(this IContainer container, IList<T> listOfElements) // Dodanie zakresu elementów do obiektu kontenerowego
        {
            var collectionProperty = container.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IList<T>)); // Sprawdzamy, czy obiekt posiada właściwość będącą listą obiektów typu T
            if (collectionProperty != null)
            {
                // Jeśli tak, uzyskujemy dostęp do listy i dodajemy do niej cały zakres elementów
                var collection = (IList<T>)collectionProperty.GetValue(container);
                foreach (var item in listOfElements)
                {
                    collection.Add(item);
                }
            }

            // Zwracamy obiekt kontenerowy po dodaniu zakresu elementów
            return container;
        }
    }

    //----------============ Zad 02 ============----------↑

    internal class Program
    {
        //----------============ Zad 01 ============----------↓

        public abstract class Person : IDisplayable, IContainer // Abstrakcyjna klasa reprezentująca osobę, implementująca interfejsy IDisplayable i IContainer
        {
            // Właściwości reprezentujące dane osobowe
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }

            // Konstruktor inicjalizujący dane osobowe
            public Person(string firstName, string lastName, DateTime dateOfBirth)
            {
                FirstName = firstName;
                LastName = lastName;
                DateOfBirth = dateOfBirth;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący osobę
            public override string ToString()
            {
                return $"{FirstName} {LastName} - {DateOfBirth.ToShortDateString()}";
            }
        }

        public class Lecturer : Person // Klasa reprezentująca wykładowcę, dziedzicząca po klasie Person
        {
            // Dodatkowe właściwości charakterystyczne dla wykładowcy
            public string AcademicTitle { get; set; }
            public string Position { get; set; }

            // Konstruktor inicjalizujący dane wykładowcy i dziedziczący dane osobowe z klasy Person
            public Lecturer(string firstName, string lastName, DateTime dateOfBirth, string academicTitle, string position) : base(firstName, lastName, dateOfBirth)
            {
                AcademicTitle = academicTitle;
                Position = position;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący wykładowcę
            public override string ToString()
            {
                return $"Lecturer | {base.ToString()} {Position}\n{AcademicTitle} ";
            }
        }

        public class Student : Person // Klasa reprezentująca studenta, dziedzicząca po klasie Person
        {
            private static int id; // Prywatna statyczna zmienna identyfikująca studenta

            // Dodatkowe właściwości charakterystyczne dla studenta
            public IList<FinalGrade> Grades { get; set; } = new List<FinalGrade>();
            public int Semestr { get; set; }
            public int Group { get; set; }
            public int IndexId { get; set; }
            public string Specialization { get; set; }
            public double AverageGrades { get; }
            // Konstruktor inicjalizujący dane studenta i dziedziczący dane osobowe z klasy Person
            public Student(string firstName, string lastName, DateTime dateOfBirth, string specialization, int group, int semestr = 1) : base(firstName, lastName, dateOfBirth)
            {
                Semestr = semestr;
                Group = group;
                Specialization = specialization;
            }
            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący studenta
            public override string ToString()
            {
                return $"Student | {base.ToString()} {Semestr} {Group} {Specialization} - {IndexId}\n{AverageGrades}";
            }
        }

        public class Subject // Klasa reprezentująca przedmiot
        {
            // Właściwości reprezentujące dane przedmiotu
            public string Name { get; set; }
            public string Specialization { get; set; }
            public int Semestr { get; set; }
            public int HoursCount { get; set; }

            // Konstruktor inicjalizujący dane przedmiotu
            public Subject(string name, string specialization, int semestr, int hoursCount)
            {
                Name = name;
                Specialization = specialization;
                Semestr = semestr;
                HoursCount = hoursCount;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący przedmiot
            public override string ToString()
            {
                return $"Subject | {Name} {Specialization} {Semestr} - {HoursCount}";
            }
        }

        public class FinalGrade // Klasa reprezentująca ocenę końcową
        {
            // Właściwości reprezentujące dane oceny końcowej
            public Subject Subject { get; set; }
            public DateTime Date { get; set; }
            public double Value { get; set; }
            // Konstruktor inicjalizujący dane oceny końcowej
            public FinalGrade(Subject subject, double value, DateTime date)
            {
                Subject = subject;
                Date = date;
                Value = value;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący ocenę końcową
            public override string ToString()
            {
                return $"FinalGrade:\n{Subject} | {Date.ToShortDateString()} {Value}";
            }
        }

        // Klasa reprezentująca jednostkę organizacyjną
        public class OrganizationUnit : IDisplayable, IContainer
        {
            // Właściwości reprezentujące dane jednostki organizacyjnej
            public string Name { get; set; }
            public string Address { get; set; }
            public IList<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

            // Konstruktor inicjalizujący dane jednostki organizacyjnej
            public OrganizationUnit(string name, string address, IList<Lecturer> lecturers)
            {
                Name = name;
                Address = address;
                Lecturers = lecturers;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący jednostkę organizacyjną
            public override string ToString()
            {
                string lecturers = "\nLecturers:\n";

                foreach (var lecturer in Lecturers)
                {
                    lecturers += lecturer.ToString() + "\n";
                }
                return $"OrganizationUnit | {Name} {Address} {lecturers}";
            }
        }


        public class Department : IDisplayable, IContainer // Klasa reprezentująca wydział lub dział w instytucji edukacyjnej
        {
            // Właściwości reprezentujące dane wydziału lub działu
            public string Name { get; set; }
            public Person Dean { get; set; }
            public IList<OrganizationUnit> OrganizationUnits { get; set; } = new List<OrganizationUnit>();
            public IList<Subject> Subjects { get; set; } = new List<Subject>();
            public IList<Student> Students { get; set; } = new List<Student>();

            // Konstruktor inicjalizujący dane wydziału lub działu
            public Department(string name, Person dean, IList<Subject> subjects, IList<Student> students)
            {
                Name = name;
                Dean = dean;
                Subjects = subjects;
                Students = students;
            }

            // Przesłonięta metoda ToString zwracająca sformatowany ciąg znaków reprezentujący wydział lub dział
            public override string ToString()
            {
                return $"Department | {Name} {Dean}";
            }
        }

        //----------============ Zad 01 ============----------↑

        static void Main(string[] args)
        {
            //----------============ Zad 01 ============----------↓

            Console.WriteLine("----------============ Zad 01 ============----------↓\n");
            Student student1 = new Student("Jan", "Kowalski", new DateTime(1995, 1, 1), "Informatyka", 1);
            Student student2 = new Student("Piotr", "Nowak", new DateTime(1990, 1, 1), "Matematyka", 3, 2);
            Person student3 = new Student("Adam", "Bedrnarski", new DateTime(1993, 1, 1), "Informatyka", 1, 2);
            Subject subject1 = new Subject("Programowanie obiektowe", "Informatyka", 4, 30);
            Subject subject2 = new Subject("Bazy danych", "Informatyka", 4, 30);
            Subject subject3 = new Subject("Algebra", "Matematyka", 1, 15);
            Subject subject4 = new Subject("Analiza", "Matematyka", 1, 30);
            FinalGrade grade1 = new FinalGrade(subject1, 4.5d, DateTime.Now.AddDays(30));
            FinalGrade grade2 = new FinalGrade(subject1, 5d, DateTime.Now.AddDays(10));
            FinalGrade grade3 = new FinalGrade(subject2, 3.5d, DateTime.Now.AddDays(50));
            FinalGrade grade4 = new FinalGrade(subject2, 3.0d, DateTime.Now.AddDays(20));
            FinalGrade grade5 = new FinalGrade(subject3, 5d, DateTime.Now.AddDays(10));
            FinalGrade grade6 = new FinalGrade(subject3, 4.0d, DateTime.Now.AddDays(10));
            FinalGrade grade7 = new FinalGrade(subject4, 4.0d, DateTime.Now.AddDays(30));
            FinalGrade grade8 = new FinalGrade(subject4, 3.5d, DateTime.Now.AddDays(20));
            Lecturer lecturer1 = new Lecturer("Krzysztof", "Nowakowski", new DateTime(1978, 12, 12), "dr inż.",
            "Adiunkt");
            Lecturer lecturer2 = new Lecturer("Jan", "Kowalski", new DateTime(1960, 10, 12), "Prof. dr hab. inż.",
            "Profesor");
            Lecturer lecturer3 = new Lecturer("Adam", "Nowakowski", new DateTime(1968, 2, 12), "dr inż.", "Adiunkt");
            Lecturer lecturer4 = new Lecturer("Arkadiusz", "Bednarski", new DateTime(1969, 1, 12), "dr hab. inż.",
            "Profesor");
            Lecturer lecturer5 = new Lecturer("Janusz", "Wiśniewski", new DateTime(1988, 2, 12), "dr inż.", "Adiunkt");
            Lecturer lecturer6 = new Lecturer("Dariusz", "Kowalewski", new DateTime(1979, 1, 12), "dr hab. inż.",
            "Profesor");
            var lecturerList1 = new List<Lecturer> { lecturer1, lecturer2 };
            var lecturerList2 = new List<Lecturer> { lecturer4, lecturer3 };
            OrganizationUnit organizationUnit1 = new OrganizationUnit("Katedra Informatyki",
             "Częstochowa", lecturerList1);
            OrganizationUnit organizationUnit2 = new OrganizationUnit("Kadera Inteligentnych Systemów Informatycznych",
             "Częstochowa", lecturerList2);
            Console.WriteLine(organizationUnit1);
            Console.WriteLine(organizationUnit2);
            Lecturer dean = new Lecturer("Tadeusz", "Nowak", new DateTime(1955, 1, 12), "Prof. dr hab. inż.",
             "Profesor");
            Department department = new Department("Wydział Inżynierii Mechanicznej i Informatyki", dean, new List<Subject>() { subject1, subject2 }, new List<Student>() { student1, student2, (Student)student3 });
            Console.WriteLine(department);

            //----------============ Zad 01 ============----------↑


            //----------============ Zad 02 ============----------↓

            Console.WriteLine("\n----------============ Zad 02 ============----------↓\n");

            student1.Add(grade1);
            student2.AddRange(new List<FinalGrade> { grade2, grade3 });
            ((Student)student3).AddRange(new List<FinalGrade> { grade4 });
            Department department2 = new Department("WE", new Lecturer("Jan", "Nowak", DateTime.Now.AddYears(-56), "dr hab.", "dziekan"), new List<Subject>() { subject3, subject4 }, new List<Student>() { student1, student2, (Student)student3 });
            department2.AddRange(new List<OrganizationUnit> { new OrganizationUnit("IOO", "Rolnicza 2", new List<Lecturer> { lecturer5 }), new OrganizationUnit("SKL", "Miedziana 13", new List<Lecturer> { lecturer6 }) });

            department2.Add(new Student("Jacek", "Bednarski", new DateTime(1989, 2, 12), "Matematyka", 1).AddRange(new List<FinalGrade> { grade7, grade8 }) as Student);
            department2.Add(new Student("Marek", "Wiśniewski", new DateTime(2001, 12, 1), "Matematyka", 1).AddRange(new List<FinalGrade> { grade5, grade6 }) as Student);
            department2.Print();
            var obtainedStudent = department2.Get<Student>(x => x.Group == 1);
            obtainedStudent.Print();
            department2.Get<Student>().GetList<FinalGrade>(g => g.Subject.Name == "Informatyka").Print();
            department2.Add(new Subject("Paradygmaty programowania", "Informatyka", 2, 10));
            department2.Add(new Subject("Podstawy sieci komputerowych", "Informatyka", 2, 30));
            var organizationUnit = department2.Get<OrganizationUnit>(x => x.Name == "SKL");
            organizationUnit.Print();
            department2.Get<OrganizationUnit>(x => x.Name == "SKL").Add(new Lecturer("Maria", "Nowak", new DateTime(1912, 12, 1), "mgr", "Lektor"));
            organizationUnit.Print();
            department2.Get<OrganizationUnit>(x => x.Name == "SKL").Remove<Lecturer>(l => l.FirstName == "Maria");
            department2.GetList<OrganizationUnit>(ou => ou.Name == "SKL").Print();

            //----------============ Zad 02 ============----------↑

            Console.ReadKey();
        }
    }
}
