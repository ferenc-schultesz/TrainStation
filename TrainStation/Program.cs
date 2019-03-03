using System;
using System.Diagnostics;
using TrainStation.Services;
using TrainStation.Utils;

namespace TrainStation
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITrainStationSuggestorService suggestorWithList;
            ITrainStationSuggestorService suggestorWithTrie;
            IFileHandler ffileHAndler = new FileHandler();
            try
            {
                // Init suggestors
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                suggestorWithList = new TrieSuggestorService("Data\\TrainStations.txt", ffileHAndler);
                stopwatch.Stop();
                Console.WriteLine("Trainstation suggester has been initialised.");
                Console.Write($"It took {stopwatch.ElapsedTicks} ticks with list data structure ");
                stopwatch.Reset();
                stopwatch.Start();
                suggestorWithTrie = new TrieSuggestorService("Data\\TrainStations.txt", ffileHAndler);
                stopwatch.Stop();
                Console.WriteLine($"and {stopwatch.ElapsedTicks} ticks with trie data structure.");
                stopwatch.Reset();

                // Get user input and search
                string searchString;
                Console.WriteLine("Enter full or partial station name: ");
                searchString = Console.ReadLine();

                // search with list 
                stopwatch.Start();
                var listSuggestions = suggestorWithList.GetSuggestions(searchString);
                stopwatch.Stop();
                Console.WriteLine($"------------- Search took {stopwatch.ElapsedTicks} ticks with list -------------");
                Console.Write("Possible next letters: ");
                foreach(char letter in listSuggestions.NextLetters)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.Write("Possible Stations: ");
                foreach (string station in listSuggestions.Stations)
                {
                    Console.Write(station + " ");
                }
                Console.WriteLine("");
                stopwatch.Reset();

                // search with trie
                stopwatch.Start();
                var trieSuggestions = suggestorWithTrie.GetSuggestions(searchString);
                stopwatch.Stop();
                Console.WriteLine($"Search took {stopwatch.ElapsedTicks} ticks with trie ------------------------------------");
                Console.Write("Possible next letters: ");
                foreach (char letter in trieSuggestions.NextLetters)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.Write("Possible Stations: ");
                foreach (string station in trieSuggestions.Stations)
                {
                    Console.Write(station + " ");
                }
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 100000; i++)
                {

                }
                Console.WriteLine($"Loop test 1: {stopwatch.ElapsedTicks}");
                stopwatch.Reset();

                stopwatch.Start();
                for (int i = 0; i < 100000; i++)
                {

                }
                Console.WriteLine($"Loop test 2: {stopwatch.ElapsedTicks}");
                stopwatch.Reset();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}
