using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestApp.Models;
using TestApp.Utils;

namespace TestApp
{
    class Program
    {
        static List<Towar> towary = new List<Towar>();
        static CSVReader csvReader = new CSVReader();

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Aplikacja testowa\n");
            Console.WriteLine("Naciśnij ESCAPE aby wyjść");
            try
            {
                while (true)
                {
                    if (towary.Count == 0)
                    {
                        Console.WriteLine("Naciśnij dowolny klawisz aby odczytać plik .csv");
                        Console.ReadKey();
                        towary = csvReader.readAndParseCSV();
                    }
                    //Dostępne operacje
                    Console.WriteLine("\n Dostępne funkcję: ");
                    Console.WriteLine("1 -> Zapisz do XML posortowane wg nazwy");
                    Console.WriteLine("2 -> Zapisz do XML gdzie cena większa od");
                    Console.WriteLine("3 -> Wyszukaj frazę w opisie");
                    char keyPressed = Console.ReadKey().KeyChar;
                    String fileName;
                    switch (keyPressed)
                    {
                        case '1':
                            Console.WriteLine("\nPodaj nazwę pliku do zapisu: ");
                            fileName = Console.ReadLine();
                            saveToXMLByName(fileName);
                            break;
                        case '2':
                            Console.WriteLine("\nPodaj minimalną cenę (int lub float): ");
                            float minPrice = float.Parse(Console.ReadLine());
                            Console.WriteLine("\nPodaj nazwę pliku do zapisu: ");
                            fileName = Console.ReadLine();
                            saveToXMLWithPrice(minPrice, fileName);
                            break;
                        case '3':
                            Console.Write("\nPodaj ciąg znaków którym chcesz przefiltrować opisy i naciśnij ENTER: ");
                            String phase = Console.ReadLine();
                            filterByPhase(phase);
                            break;
                        case (char)Keys.Escape:
                            Application.Exit();
                            break;
                        default:
                            Console.WriteLine("Wciśnięto nieobsługiwany klawisz");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nieoczekiwany błąd w aplikacji: " + ex.Message);
                Console.WriteLine("Naciśnij dowolny klawisz aby zamknąć");
                Console.ReadKey();

            }
        }
        static private void saveToXMLByName(String fileName)
        {
            var sortedList = Program.towary.OrderBy(towar => towar.Nazwa).ToList<Towar>();
            XMLWriter xmlWriter = new XMLWriter();
            xmlWriter.SaveXML(sortedList, fileName);
        }
        static private void saveToXMLWithPrice(float minPrice, String fileName)
        {
            var filteredList = Program.towary.Where(towar => towar.Cena > minPrice).ToList<Towar>();
            XMLWriter xmlWriter = new XMLWriter();
            xmlWriter.SaveXML(filteredList, fileName);
        }
        static private void filterByPhase(String phase)
        {
            var filteredList = Program.towary.Where(towar => towar.Opis.A.Contains(phase) || towar.Opis.B.Contains(phase));
            foreach (var filteredRow in filteredList)
            {
                Console.WriteLine(filteredRow.Nazwa);
            }
        }
    }
}
