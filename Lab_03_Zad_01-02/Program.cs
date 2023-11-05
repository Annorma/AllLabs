using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Lab_03_Zad_01_02.Program;

namespace Lab_03_Zad_01_02
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

        public class Catalog // Klasa reprezentująca katalog zawierający  wiele elementów.
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

        public class Author
        {
            // Klasa reprezentująca autora książki

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Nationality { get; set; }

            public Author() // Konstruktor domyślny.
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                Nationality = string.Empty;
            }

            public Author(string firstName, string lastName, string nationality) // Konstruktor z parametrami.
            {
                FirstName = firstName;
                LastName = lastName;
                Nationality = nationality;
            }
            public override string ToString() // Przesłonięta metoda ToString zwraca łańcuch znaków reprezentujący autora.
            {
                return $"Author | {FirstName} {LastName} : {Nationality}";
            }
        }

        static void Main(string[] args)
        {
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

            Console.ReadKey();
        }
    }
}
