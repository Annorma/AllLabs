using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Lab_03_Zad_01_02_New.Program;

namespace Lab_03_Zad_01_02_New
{
    internal class Program
    {


        public abstract class Item // Klasa bazowa dla elementów w katalogu
        {
            // Zawiera podstawowe właściwości i metody wspólne dla wszystkich elementów

            // Pola opisujące element.
            protected int _id;
            protected string _title;
            protected string _publisher;
            protected DateTime _dateOfIssue;

            public Item() // Konstruktor domyślny.
            {
                Id = 0;
                Title = string.Empty;
                Publisher = string.Empty;
                _dateOfIssue = DateTime.MinValue;
            }
            public Item(int id, string title, string publisher, DateTime dateOfIssue) // Konstruktor z parametrami.
            {
                Id = id;
                Title = title;
                Publisher = publisher;
                DateOfIssue = dateOfIssue;
            }

            // Właściwości opisujące element.
            public int Id { get => _id; set => _id = value; }
            public string Title { get => _title; set => _title = value; }
            public string Publisher { get => _publisher; set => _publisher = value; }
            public DateTime DateOfIssue { get => _dateOfIssue; set => _dateOfIssue = value; }

            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący element.
            {
                // Zwraca informacje o elemencie
                return $"{Id} - {Title}. Publisher: {Publisher} do {DateOfIssue.ToShortDateString()}";
            }
            public void Details()
            {
                // Wyświetla informacje o elemencie w konsoli
                Console.WriteLine(ToString());
            }
            public abstract string GenerateBarCode();
        }

        public class Catalog : IItemManagment // Klasa reprezentująca katalog zawierający  wiele elementów.
        {
            public IList<Item> Items { get; set; }
            public string ThematicDepartment { get; set; }

            public Catalog(IList<Item> items) // Konstruktor katalogu z parametrem listy elementów.
            {
                Items = items;
                ThematicDepartment = string.Empty;
            }

            public Catalog(string thematicDepartment, IList<Item> items) // Konstruktor katalogu z parametrami listy elementów i działu tematycznego.
            {
                ThematicDepartment = thematicDepartment;
                Items = items;
            }
            public void AddItem(Item item) // Dodawanie nowego elementu do katalogu
            {
                Items.Add(item);
            }

            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący katalog.
            {
                return $"Catalog | {ThematicDepartment}";
            }
            public void ShowAllItems() // Metoda wyświetla wszystkie elementy w katalogu.
            {
                foreach (var item in Items)
                {
                    Console.WriteLine(item);
                }
            }
            public Item? FindItem(Expression<Func<Item, bool>> predicate)
            {
                // Metoda znajduje element w katalogu na podstawie określonego predykatu.
                return Items.FirstOrDefault(item => predicate.Compile()(item));
            }
            public Item? FindItemBy(string title)
            {
                // Metoda znajduje element w katalogu po tytule.
                return Items.FirstOrDefault(item => item.Title == title);
            }

            public Item? FindItemBy(int id)
            {
                // Metoda znajduje element w katalogu po identyfikatorze.
                return Items.FirstOrDefault(item => item.Id == id);
            }
        }

        public class Journal : Item
        {
            // Klasa reprezentująca czasopismo

            public int Number { get; set; }
            public Journal() // Konstruktor domyślny.
            {
                Number = 0;
            }
            public Journal(string title, int id, string publisher, DateTime dateOfIssue, int number) : base(id, title, publisher, dateOfIssue) // Konstruktor z parametrami.
            {
                Number = number;
            }

            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący czasopismo.
            {
                return $"Journal | {base.ToString()}, Number: {Number}";
            }
            public override string GenerateBarCode() // Metoda generuje kod kreskowy dla czasopisma.
            {
                return $"Journal Code";
            }
        }
        public class Book : Item
        {
            // Klasa reprezentująca książkę

            public int PageCount { get; set; }
            public IList<Author> Authors { get; set; }
            public Book() // Konstruktor domyślny.
            {
                PageCount = 0;
                Authors = new List<Author>();
            }
            public Book(string title, int id, string publisher, DateTime dateOfIssue, int pageCount, IList<Author> authors) : base(id, title, publisher, dateOfIssue) // Konstruktor z parametrami.
            {
                PageCount = pageCount;
                Authors = authors;
            }
            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący książkę.
            {
                string authors = "\nAuthors:\n";

                foreach (var author in Authors)
                {
                    authors += author.ToString() + "\n";

                }
                return $"Book | {base.ToString()}, PageCount: {PageCount} {authors}";
            }
            public override string GenerateBarCode() // Metoda generuje kod kreskowy dla książki.
            {
                return $"Book Code";
            }

            public void AddAuthor(Author author) // Dodawanie nowego autora do książki
            {
                Authors.Add(author);
            }
        }

        public class Person
        {
            // Klasa reprezentująca osobę
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Person()
            {
                FirstName = string.Empty;
                LastName = string.Empty;
            }
            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący osobę.
            {
                return $"{FirstName} {LastName}";
            }

            public void Details()
            {
                Console.WriteLine(ToString());
            }
        }

        public class Author : Person
        {
            // Klasa reprezentująca autora książki

            public string Nationality { get; set; }

            public Author() // Konstruktor domyślny.
            {
                Nationality = string.Empty;
            }

            public Author(string firstName, string lastName, string nationality) : base(firstName, lastName)// Konstruktor z parametrami.
            {
                Nationality = nationality;
            }
            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący autora.
            {
                return $"Author | {base.ToString()} : {Nationality}";
            }
        }

        public class Librarian : Person
        {
            // Klasa reprezentująca bibliotekarza

            public DateTime HireDate { get; set; }
            public decimal Salary { get; set; }

            public Librarian() // Konstruktor domyślny.
            {
                HireDate = DateTime.MinValue;
                Salary = 0;
            }

            public Librarian(string firstName, string lastName, DateTime hireDate, decimal salary) : base(firstName, lastName) // Konstruktor z parametrami.
            {
                HireDate = hireDate;
                Salary = salary;
            }
            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący bibliotekarza.
            {
                return $"Librarian | {base.ToString()} : {HireDate.ToShortDateString()} - {Salary}";
            }
        }

        public interface IItemManagment
        {
            // Interfejs definiuje operacje związane z zarządzaniem przedmiotami w bibliotece.

            void ShowAllItems(); // Metoda wyświetlająca wszystkie przedmioty w bibliotece.
            Item? FindItemBy(int id); // Metoda wyszukująca przedmiot po identyfikatorze.
            Item? FindItemBy(string title); // Metoda wyszukująca przedmiot po tytule.
            Item? FindItem(Expression<Func<Item, bool>> predicate); // Metoda wyszukująca przedmiot za pomocą predykatu.
        }

        public class Library : IItemManagment
        {
            // Klasa reprezentująca bibliotekę.

            public string Adress { get; set; } // Adres biblioteki.
            public IList<Librarian> Librarians { get; set; } // Lista bibliotekarzy pracujących w bibliotece.
            public IList<Catalog> Catalogs { get; set; } // Lista katalogów w bibliotece.

            public Library() // Konstruktor domyślny, inicjalizuje właściwości obiektu.
            {
                Adress = string.Empty;
                Librarians = new List<Librarian>();
                Catalogs = new List<Catalog>();
            }

            public Library(string adress, IList<Librarian> librarians, IList<Catalog> catalogs) // Konstruktor, który przyjmuje adres, listę bibliotekarzy i listę katalogów jako parametry.
            {
                Adress = adress;
                Librarians = librarians;
                Catalogs = catalogs;
            }

            public void AddLibrarian(Librarian librarian) // Metoda dodająca bibliotekarza do listy bibliotekarzy.
            {
                if (librarian != null)
                    Librarians.Add(librarian);
            }

            public void ShowAllLibrarians() // Metoda wyświetlająca wszystkich bibliotekarzy w bibliotece.
            {
                foreach (var librarian in Librarians)
                {
                    Console.WriteLine(librarian);
                }
            }

            public void AddCatalog(Catalog catalog) // Metoda dodająca katalog do biblioteki.
            {
                Catalogs.Add(catalog);
            }

            public void AddItem(Item item, string thematicDepartment) // Metoda dodająca przedmiot do katalogu w określonym dziale tematycznym.
            {
                Catalog? catalog = Catalogs.FirstOrDefault(c => c.ThematicDepartment == thematicDepartment);

                if (catalog == null)
                {
                    // Jeśli katalog nie istnieje, tworzymy nowy katalog i dodajemy do niego element.
                    catalog = new Catalog(thematicDepartment, new List<Item>());
                    Catalogs.Add(catalog);
                }

                // Dodajemy element do katalogu.
                catalog.AddItem(item);
            }

            public void ShowAllItems() // Metoda wyświetlająca wszystkie przedmioty w bibliotece.
            {
                foreach (var catalog in Catalogs)
                {
                    Console.WriteLine(catalog);
                    catalog.ShowAllItems();
                }
            }

            public Item? FindItemBy(int id) // Metoda znajduje element w bibliotece po identyfikatorze.
            {
                return Catalogs.SelectMany(c => c.Items).FirstOrDefault(item => item.Id == id);
            }

            public Item? FindItemBy(string title) // Metoda znajduje element w bibliotece po tytule.
            {
                return Catalogs.SelectMany(c => c.Items).FirstOrDefault(item => item.Title == title);
            }

            public Item? FindItem(Expression<Func<Item, bool>> predicate) // Metoda znajduje element w bibliotece na podstawie określonego predykatu.
            {
                return Catalogs.SelectMany(c => c.Items).FirstOrDefault(item => predicate.Compile()(item));
            }

            public override string ToString() // Metoda zwracająca reprezentację tekstową biblioteki.
            {
                return $"Library | {Adress}";
            }
        }

        public class City
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public IList<Library> Libraries { get; set; }
            public City()
            {
                Index = 0;
                Name = string.Empty;
                Libraries = new List<Library>();
            }
            public City(int index, string name, IList<Library> libraries)
            {
                Index = index;
                Name = name;
                Libraries = libraries;
            }
            public override string ToString()
            {
                return $"City | {Name} {Index}";
            }
        }

        static void Main(string[] args)
        {
            //----------============ Zad 01 ============----------↓

            // Tworzenie przykładowych elementów katalogu
            Item item1 = new Journal("JAISCR", 1, "Springer", new DateTime(2000, 1, 1), 1);
            Author author = new Author("Robert", "Cook", "Polish");
            Item item2 = new Book("Agile C#", 2, "SPRINGER", new DateTime(2015, 1, 1), 500,
             new List<Author>() { author });

            ((Book)item2).AddAuthor(author); // Dodawanie autora do książki

            var bookBarCode = ((Book)item2).GenerateBarCode(); // Generowanie kodu kreskowego dla książki
            var journalBarCode = ((Journal)item1).GenerateBarCode(); // Generowanie kodu kreskowego dla czasopisma

            Console.WriteLine($"{item1} \r\n Barcode: {journalBarCode}"); // Wyświetlanie informacji o czasopiśmie
            Console.WriteLine($"{item2} \r\n Barcode: {bookBarCode}"); // Wyświetlanie informacji o książce
            IList<Item> items = new List<Item>();
            items.Add(item1);
            items.Add(item2);
            Catalog catalog = new Catalog("IT C# development", items);
            catalog.AddItem(new Journal("Neurocomputing", 1, "IEEE", new DateTime(2020, 1, 1), 1)); // Dodawanie czasopisma do katalogu

            Console.WriteLine(catalog); // Wyświetlanie informacji o katalogu
            catalog.ShowAllItems(); // Wyświetlanie wszystkich elementów w katalogu

            //----------============ Zad 01 ============----------↑


            //----------============ Zad 02 ============----------↓

            //--- find position
            string searchedValue = "Agile C#";
            Item? foundedItemById = catalog.FindItem(item => item.Id == 1); // Wyszukuje element o określonym ID.
            Item? foundedItemByTitle = catalog.FindItem(item => item.Title == searchedValue); // Wyszukuje element po tytule.
            Item? foundedItemByDateRange = catalog.FindItem(item => item.DateOfIssue >= new DateTime(2014, 12, 31) &&
            item.DateOfIssue <= new DateTime(2015, 12, 31)); // Wyszukuje elementy wydane w określonym zakresie dat.
            Console.WriteLine("++++++++++++++++++++++++++++++++++");
            Console.WriteLine(foundedItemById); // Wyświetla znalezione elementy.
            Console.WriteLine(foundedItemByTitle);
            Console.WriteLine(foundedItemByDateRange);

            Item? foundedItemByIdOld = catalog.FindItemBy(1); // Wyszukuje element o określonym ID za pomocą starszego sposobu.
            Item? foundedItemByTitleOld = catalog.FindItemBy(searchedValue); // Wyszukuje element po tytule za pomocą starszego sposobu.
            Console.WriteLine("Found old way");
            Console.WriteLine(foundedItemByIdOld);
            Console.WriteLine(foundedItemByTitleOld);
            Console.WriteLine("++++++++++++++++++++++++++++++++++");

            Person librarian = new Librarian("John", "Kowalsky", DateTime.Now.Date, 2000); // Tworzy obiekt bibliotekarza.
            Library library = new Library("Czestochowa, Armii Krajowej 36", new List<Librarian>(), new List<Catalog>()); // Tworzy obiekt biblioteki.
            library.AddLibrarian((Librarian)librarian); // Dodaje bibliotekarza do biblioteki.
            library.ShowAllLibrarians(); // Wyświetla wszystkich bibliotekarzy.

            Catalog catalog2 = new Catalog("Novels", new List<Item>()); // Tworzy nowy katalog dla powieści.
            library.AddCatalog(catalog2); // Dodaje nowy katalog do biblioteki.
            library.AddCatalog(catalog); // Dodaje katalog do biblioteki.

            Item newItem = new Book("Song of Ice and Fire", 4, "Publisher", new DateTime(2011, 1, 1), 800,
             new List<Author>() { author }); // Tworzy nową książkę.
            library.AddItem(newItem, "Novels"); // Dodaje nową książkę do katalogu powieści.

            Console.WriteLine(library); // Wyświetla informacje o bibliotece.
            Console.WriteLine("===========================All Items=======================\r\n");
            library.ShowAllItems(); // Wyświetla wszystkie elementy w bibliotece.
            Console.WriteLine("===========================FIND BY=======================\r\n");

            var foundedById = library.FindItemBy(4); // Wyszukuje element o określonym ID w bibliotece.
            var foundedByTitle = library.FindItemBy(searchedValue); // Wyszukuje element po tytule w bibliotece.
            var foundedByLambda = library.FindItem(x => x.Publisher == "Springer"); // Wyszukuje elementy w bibliotece za pomocą predykatu.
            Console.WriteLine(foundedById);
            Console.WriteLine(foundedByTitle);
            Console.WriteLine(foundedByLambda);

            //----------============ Zad 02 ============----------↑

            Console.ReadKey();
        }
    }
}

