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
            try
            {
                // Init suggesters
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                suggestorWithList = new ListSuggestorService("Data\\TrainStations.txt", new FileHandler());
                stopwatch.Stop();
                Console.WriteLine("Trainstation suggester has been initialised.");
                Console.Write($"It took {stopwatch.ElapsedTicks} ticks with list data structure ");
                stopwatch.Reset();
                stopwatch.Start();
                suggestorWithTrie = new TrieSuggestorService("Data\\TrainStations.txt", new FileHandler());
                stopwatch.Stop();
                Console.WriteLine($"and {stopwatch.ElapsedTicks} ticks with trie data structure.");
                stopwatch.Reset();

                // Get user input and search
                string searchString;
                Console.WriteLine("Enter full or partial station name: ");
                searchString = Console.ReadLine();

                // search with list 
                stopwatch.Start();
                var suggestions = suggestorWithList.GetSuggestions(searchString);
                stopwatch.Stop();
                Console.WriteLine($"------------- Search took {stopwatch.ElapsedTicks} ticks with list -------------");
                Console.Write("Possible next letters: ");
                foreach(char letter in suggestions.NextLetters)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.Write("Possible Stations: ");
                foreach (string station in suggestions.Stations)
                {
                    Console.Write(station + " ");
                }
                Console.WriteLine("");
                stopwatch.Reset();

                stopwatch.Start();
                suggestions = suggestorWithTrie.GetSuggestions(searchString);
                stopwatch.Stop();
                Console.WriteLine($"Search took {stopwatch.ElapsedTicks} ticks with trie ------------------------------------");
                Console.Write("Possible next letters: ");
                foreach (char letter in suggestions.NextLetters)
                {
                    Console.Write(letter + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.Write("Possible Stations: ");
                foreach (string station in suggestions.Stations)
                {
                    Console.Write(station + " ");
                }
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
