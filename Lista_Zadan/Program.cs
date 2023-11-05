using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Lista_Zadan
{
    internal class Program
    {

        public class Zadanie // Klasa reprezentująca zadanie
        {
            private string etykieta;
            private string tresc;
            private DateTime terminWykonania;
            private string status;
            private bool isDone;
            public Zadanie() // Konstruktor domyślny
            {
                etykieta = "none";
                tresc = "none";
                terminWykonania = DateTime.Now;
                status = "none";
                isDone = false;
            }

            public Zadanie(string etykieta, string tresc, DateTime terminWykonania, string status, bool isDone) // Konstruktor z argumentami
            {
                this.etykieta = etykieta;
                this.tresc = tresc;
                this.terminWykonania = terminWykonania;
                this.status = status;
                this.isDone = isDone;
            }

            // Właściwości
            public string Etykieta
            {
                get { return etykieta; }
                set { etykieta = value; }
            }
            public string Tresc
            {
                get { return tresc; }
                set { tresc = value; }
            }
            public DateTime TerminWykonania
            {
                get { return terminWykonania; }
                set { terminWykonania = value; }
            }
            public string Status
            {
                get { return status; }
                set { status = value; }
            }
            public bool IsDone
            {
                get { return isDone; }
                set { isDone = value; }
            }

            // Przesłonięcie metody ToString
            public override string ToString()
            {
                return isDone ? $"{Status} | {Etykieta} - Zrobione\n[{Tresc}]" : $"{Status} | {Etykieta} - do {TerminWykonania.ToShortDateString()}\n[{Tresc}]";
            }
        }

        public class ListaZadan // Klasa reprezentująca listę zadań
        {
            private List<Zadanie> zadania;
            private int current = 0;
            private int maxIndex;
            private string filePath = "moja_lista_zadan.json";
            ConsoleKeyInfo KeyPress = new ConsoleKeyInfo();

            public ListaZadan() // Konstruktor
            {
                zadania = new List<Zadanie>();
            }

            public void Menu() // Metoda obsługująca menu
            {
                while (true)
                {
                    KeyPress = Console.ReadKey(true);
                    switch (KeyPress.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (current > 0) { current--; }
                            else { current = maxIndex; }
                            Show();
                            break;
                        case ConsoleKey.DownArrow:
                            if (current < maxIndex) { current++; }
                            else { current = 0; }
                            Show();
                            break;
                        case ConsoleKey.D1:
                            AddTask();
                            break;
                        case ConsoleKey.D2:
                            MarkAsDone(current);
                            break;
                        case ConsoleKey.D3:
                            MarkAsNotDone(current);
                            break;
                        case ConsoleKey.D4:
                            SetNewStatus(current);
                            break;
                        case ConsoleKey.D5:
                            SetNewTermin(current);
                            break;
                        case ConsoleKey.D6:
                            SortTasksByStatus();
                            break;
                        case ConsoleKey.D7:
                            SaveMyToDoList();
                            break;
                        case ConsoleKey.D8:
                            LoadMyToDoList();
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }
            }

            public void Show() // Metoda wyświetlająca listę zadań
            {
                Console.Clear();

                for (int i = 0; i < zadania.Count; i++)
                {
                    if (i == current)
                    {
                        SetColor(ConsoleColor.Cyan);
                    }
                    else
                    {
                        SetColor(ConsoleColor.White);
                    }
                    Console.WriteLine(zadania[i]);
                }

                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========================");
                Console.WriteLine("Instrukcje:");
                Console.WriteLine("Up/Down - Poruszanie się po zadaniach");
                Console.WriteLine("1 - Dodaj nowe zadanie");
                Console.WriteLine("2 - Oznacz zadanie jako zrobione");
                Console.WriteLine("3 - Oznacz zadanie jako niewykonane");
                Console.WriteLine("4 - Zmień status zadania");
                Console.WriteLine("5 - Zmień termin wykonania zadania");
                Console.WriteLine("6 - Sortuj zadania według statusu");
                Console.WriteLine("7 - Zapisz listę zadań do pliku");
                Console.WriteLine("8 - Wczytaj listę zadań z pliku");
                Console.WriteLine("Esc - Wyjście z programu");
                Console.WriteLine();

                Console.ResetColor();

            }

            public void SetColor(ConsoleColor color) // Metoda ustawiająca kolor konsoli
            {
                Console.ForegroundColor = color;
            }

            public void ClearAll() // Metoda czyści konsolę i przywraca domyślny kolor
            {
                Console.ResetColor();
                Console.Clear();
            }

            public void AddTask(string etykieta, string tresc, DateTime terminWykonania, string status, bool isDone) // Metoda dodająca nowe zadanie do listy (dla main)
            {
                zadania.Add(new Zadanie(etykieta, tresc, terminWykonania, status, isDone));
                maxIndex = zadania.Count - 1;
            }
            public void AddTask() // Metoda dodająca nowe zadanie do listy (dla konsoli)
            {
                ClearAll();
                Console.Write("Etykieta: ");
                string etykieta = Console.ReadLine();

                Console.Write("Treść: ");
                string tresc = Console.ReadLine();

                Console.Write("Termin wykonania (RRRR-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime terminWykonania))
                {
                    Console.Write("Status: ");
                    string status = Console.ReadLine();

                    Console.Write("Czy zrobione (Tak/Nie): ");
                    string isDoneInput = Console.ReadLine();
                    bool isDone = isDoneInput.Equals("Tak", StringComparison.OrdinalIgnoreCase);

                    zadania.Add(new Zadanie(etykieta, tresc, terminWykonania, status, isDone));
                    maxIndex = zadania.Count - 1;
                }
                else
                {
                    Console.WriteLine("Wrong date format. Task adding canceled.");
                    Console.ReadKey();
                }
                Show();
            }

            public void MarkAsDone(int index) // Metoda oznaczająca zadanie jako zrobione
            {
                zadania[index].IsDone = true;
                Show();
            }

            public void MarkAsNotDone(int index) // Metoda oznaczająca zadanie jako niewykonane
            {
                zadania[index].IsDone = false;
                Show();
            }

            public void SetNewTermin(int index) // Metoda zmieniająca termin wykonania zadania
            {
                ClearAll();
                Console.WriteLine(zadania[current]);
                Console.Write("Nowy termin wykonania (RRRR-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime terminWykonania))
                {
                    zadania[index].TerminWykonania = terminWykonania;
                }
                else
                {
                    Console.WriteLine("Wrong date format. Task adding canceled.");
                    Console.ReadKey();
                }
                Show();
            }

            public void SetNewStatus(int index) // Metoda zmieniająca status zadania
            {
                ClearAll();
                Console.WriteLine(zadania[current]);
                Console.Write("Nowy status: ");
                string status = Console.ReadLine();
                zadania[index].Status = status;
                Show();
            }

            public void SortTasksByStatus() // Metoda sortująca zadania według statusu
            {
                zadania = zadania.OrderBy(z => z.Status).ToList();
                Show();
            }

            public void SaveMyToDoList() // Metoda zapisująca listę zadań do pliku
            {
                ClearAll();
                Console.WriteLine("Jeśli NIE podasz nazwy, nazwa pliku zostanie wybrana automatycznie!");
                Console.Write("Podaj nazwę pliku dla zapisania listy zadań (bez rozszerzenia .json): ");
                string customFileName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(customFileName))
                {
                    filePath = customFileName + ".json";
                }

                try
                {
                    string json = JsonConvert.SerializeObject(zadania);
                    File.WriteAllText(filePath, json);
                    Console.WriteLine("Zadania zostały zapisane do pliku.");
                    Console.ReadKey();
                    Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wystąpił błąd podczas zapisywania listy zadań: " + ex.Message);
                }
            }

            public void LoadMyToDoList() // Metoda wczytująca listę zadań z pliku
            {
                ClearAll();
                Console.WriteLine("Podaj nazwę pliku dla wczytania listy zadań (bez rozszerzenia .json): ");
                string customFileName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(customFileName))
                {
                    filePath = customFileName + ".json";
                }

                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        zadania = JsonConvert.DeserializeObject<List<Zadanie>>(json);
                        maxIndex = zadania.Count - 1;
                       Console.WriteLine("Zadania zostały wczytane z pliku.");
                        Console.ReadKey();
                        Show();
                    }
                    else
                    {
                        Console.WriteLine("Plik nie istnieje. Nie wczytano żadnych zadań.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wystąpił błąd podczas wczytywania listy zadań: " + ex.Message);
                }
                Show();
            }

        }
        
        static void Main(string[] args)
        {

            ListaZadan lista = new ListaZadan();
            bool exit = false;
            Console.Title = "My To Do List";

            // Dodawanie przykładowych zadań do listy
            lista.AddTask("Sklep", "Zrobic zakupy", DateTime.Now, "W trakcie", false);
            lista.AddTask("Zadanie domowe", "Zrobic lab 1 z programowania", DateTime.Now, "W trakcie", false);
            lista.AddTask("Dokumenty", "Wydrukowac wazne dokumenty", DateTime.Now, "Wazne", true);

            // Obsługa menu
            while (!exit)
            {
                Console.Clear();
                lista.Show();
                lista.Menu();

                Console.WriteLine("\n\nESC - exit");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
            }

        }
    }
}
