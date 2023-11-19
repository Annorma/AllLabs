using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_Kolokwium_01
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
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Country { get; set; }

            public Person()
            {
                Name = string.Empty;
                Surname = string.Empty;
                DateOfBirth = DateTime.MinValue;
                Country = string.Empty;
            }
            public Person(string Name, string Surname, DateTime DateOfBirth, string Country)
            {
                this.Name = Name;
                this.Surname = Surname;
                this.DateOfBirth = DateOfBirth;
                this.Country = Country;
            }

            public override string ToString()
            {
                return $"{Name} {Surname} {DateOfBirth.ToShortDateString()} {Country}";
            }
            public abstract void Work();
        }

        public class Artist : Person
        {
            public int Internship { get; set; } // Staż w latach

            public Artist(string name, string surname, DateTime dateOfBirth, string country, int internship) : base(name, surname, dateOfBirth, country)
            {
                Internship = internship;
            }
            public override string ToString()
            {
                return $"{base.ToString()} {Internship}";
            }
            public override void Work()
            {
                Console.WriteLine("Artist is working...");
            }
        }

        public class Organizer : Person
        {
            public bool IsFamous { get; set; }

            public Organizer(string name, string surname, DateTime dateOfBirth, string country, bool isFamous) : base(name, surname, dateOfBirth, country)
            {
                IsFamous = isFamous;
            }
            public override string ToString()
            {
                return IsFamous ? $"{base.ToString()} | Famous" : $"{base.ToString()} | Not famous";
            }
            public override void Work()
            {
                Console.WriteLine("Organizer is working...");
            }
        }

        public class Work : IDisplayable
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }
            public Artist Artist { get; set; }

            public Work(string name, string description, DateTime creationDate, Artist artist)
            {
                Name = name;
                Description = description;
                CreationDate = creationDate;
                Artist = artist;
            }
            public override string ToString()
            {
                return $"{Name} | {Artist.Name} {Artist.Surname} - {CreationDate.ToShortDateString()}\n{Description}";
            }
        }

        public class Exhibition : IDisplayable
        {
            private static int exhibitionCount;

            public string Name { get; set; }
            public IList<Work> Works { get; set; } = new List<Work>();
            public IList<Organizer> Organizers { get; set; } = new List<Organizer>();
            public static int ExhibitionCount { get { return exhibitionCount; } }
            static Exhibition()
            {
                exhibitionCount = 0;
            }
            public Exhibition(string name, IList<Work> works, IList<Organizer> organizers)
            {
                Name = name;
                Works = works;
                Organizers = organizers;
                exhibitionCount++;
            }

            public void FindWorkByName(string name)
            {
                foreach (var work in Works)
                {
                    if (work.Name == name)
                    {
                        Console.WriteLine(work);
                    }
                }
            }

            public Organizer? FindOrganizatorByName(string name)
            {
                return Organizers.FirstOrDefault(o => o.Name == name);
            }

            public override string ToString()
            {
                string works = "\nWorks:\n";
                string organizers = "\nOrganizers:\n";
                foreach (var work in Works)
                {
                    works += work.ToString() + "\n";
                }
                foreach (var organizer in Organizers)
                {
                    organizers += organizer.ToString() + "\n";
                }
                return $"Exhibition \"{Name}\"{works}{organizers}";
            }
        }

        static void Main(string[] args)
        {
            Artist artist1 = new Artist("Krzysztof", "Nowakowski", new DateTime(1978, 12, 12), "Poland", 10);
            Artist artist2 = new Artist("John", "Snow", new DateTime(1960, 10, 12), "USA", 11);
            Artist artist3 = new Artist("Vincent", "van Gogh", new DateTime(1853, 3, 30), "Netherlands", 37);
            Artist artist4 = new Artist("Pablo", "Picasso", new DateTime(1881, 10, 25), "Spain", 91);
            Artist artist5 = new Artist("Edvard", "Munch", new DateTime(1863, 12, 12), "Norway", 80);
            Artist artist6 = new Artist("Sandro", "Botticelli", new DateTime(1445, 3, 1), "Italy", 65);
            Artist artist7 = new Artist("Grant", "Wood", new DateTime(1891, 2, 13), "USA", 50);
            Artist artist8 = new Artist("Pablo", "Picasso", new DateTime(1881, 10, 25), "Spain", 91);
            Artist artist9 = new Artist("James", "McNeill Whistler", new DateTime(1834, 7, 11), "USA", 69);
            Artist artist10 = new Artist("Rembrandt", "van Rijn", new DateTime(1606, 7, 15), "Netherlands", 63);
            Artist artist11 = new Artist("Auguste", "Rodin", new DateTime(1840, 11, 12), "France", 77);
            Artist artist12 = new Artist("Leonardo", "da Vinci", new DateTime(1452, 4, 15), "Italy", 67);

            Organizer organizer1 = new Organizer("Abraham", "Guakamole", new DateTime(1968, 2, 12), "Puerto Rico", true);
            Organizer organizer2 = new Organizer("Ala", "Kotowska", new DateTime(1969, 1, 12), "France", false);
            Organizer organizer3 = new Organizer("Shrek", "Bagnik", new DateTime(2001, 11, 11), "Dream Works", true);
            Organizer organizer4 = new Organizer("Klaus", "Müller", new DateTime(1970, 11, 18), "Germany", true);
            Organizer organizer5 = new Organizer("Maria", "Santos", new DateTime(1985, 7, 3), "Brazil", false);
            Organizer organizer6 = new Organizer("Chen", "Li", new DateTime(1978, 5, 22), "China", true);
            Organizer organizer7 = new Organizer("Mia", "Andersson", new DateTime(1972, 12, 14), "Sweden", false);
            Organizer organizer8 = new Organizer("Ravi", "Patel", new DateTime(1989, 9, 8), "India", true);
            Organizer organizer9 = new Organizer("Isabella", "García", new DateTime(1973, 3, 30), "Mexico", false);
            Organizer organizer10 = new Organizer("Antonio", "Ricci", new DateTime(1965, 6, 9), "Italy", true);
            Organizer organizer11 = new Organizer("Sophie", "Leblanc", new DateTime(1977, 1, 21), "France", false);

            Work work1 = new Work("Mona Lisa", "She is so boring.", new DateTime(1912, 1, 1), artist1);
            Work work2 = new Work("Starry Night", "A beautiful night sky.", new DateTime(1889, 6, 1), artist2);
            Work work3 = new Work("The Persistence of Memory", "Melting clocks everywhere.", new DateTime(1931, 1, 1), artist3);
            Work work4 = new Work("Guernica", "Powerful anti-war statement.", new DateTime(1937, 3, 10), artist4);
            Work work5 = new Work("The Scream", "Captures existential angst.", new DateTime(1893, 1, 1), artist5);
            Work work6 = new Work("The Birth of Venus", "Classic depiction of the goddess.", new DateTime(1484, 1, 1), artist6);
            Work work7 = new Work("Water Lilies", "Serene pond with water lilies.", new DateTime(1919, 5, 1), artist7);
            Work work8 = new Work("American Gothic", "Iconic farmer couple.", new DateTime(1930, 1, 1), artist8);
            Work work9 = new Work("Les Demoiselles d'Avignon", "Bold and groundbreaking.", new DateTime(1907, 1, 1), artist9);
            Work work10 = new Work("Whistler's Mother", "Peaceful maternal portrait.", new DateTime(1871, 2, 1), artist10);
            Work work11 = new Work("The Night Watch", "Dramatic group portrait.", new DateTime(1642, 11, 11), artist11);
            Work work12 = new Work("The Thinker", "Contemplative bronze statue.", new DateTime(1881, 1, 1), artist12);

            var organizerList1 = new List<Organizer> { organizer1, organizer2 };
            var organizerList2 = new List<Organizer> { organizer3, organizer4, organizer5, organizer6 };
            var organizerList3 = new List<Organizer> { organizer7, organizer8, organizer9, organizer10, organizer11 };
            var worksList1 = new List<Work> { work1, work2, work3, work4, work5, work6, work7 };
            var worksList2 = new List<Work> { work8, work9, work10, work11, work12 };

            Exhibition exhibition1 = new Exhibition("Funny paintings", worksList1, organizerList1);
            Exhibition exhibition2 = new Exhibition("Not funny paintings", worksList2, organizerList2);
            Exhibition exhibition3 = new Exhibition("Gold night", worksList1, organizerList3);

            Console.WriteLine("----==== Exhibition 1 ====----\n");
            exhibition1.Print();
            Console.WriteLine("\n----==== Exhibition 2 ====----\n");
            exhibition2.Print();
            Console.WriteLine("\n----==== Exhibition 3 ====----\n");
            exhibition3.Print();

            exhibition2.FindWorkByName("American Gothic");
            var findOrganizer = exhibition1.FindOrganizatorByName(organizer1.Name);
            Console.WriteLine(findOrganizer);

            Console.ReadKey();
        }
    }
}
