using System;
using System.Diagnostics;
using System.Linq;
using System.Security;
using TrainStation.Models;
using TrainStation.Services;
using TrainStation.Utils;

namespace TrainStation
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITrainStationSuggestorService suggestorWithTrie;
            IFileHandler fileHAndler = new FileHandler("Data\\TrainStations.txt");

            try
            {
                // Init suggestors
                suggestorWithTrie = new TrieSuggestorService(fileHAndler);
                Console.WriteLine("------ Trainstation suggester has been initialised ------");

                // Tests the suggestor implementation with user input
                string userInput = TestSuggestor(ref suggestorWithTrie);

                // Meassures and displays performance difference between Trie and List implementation
                // ListSuggestorService uses List and Linq, Trie uses Trie search tree data structure
                CheckPerformance();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        public static string TestSuggestor(ref ITrainStationSuggestorService service)
        {
            // Get user input and search
            string userInput;
            Console.Write("Enter full or partial station name: ");
            userInput = Console.ReadLine();

            // search with trie
            var trieSuggestions = service.GetSuggestions(userInput);
            Console.Write("Possible next letters: ");
            foreach (char letter in trieSuggestions.NextLetters)
            {
                Console.Write(letter + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Possible Stations: ");
            var stationArray = trieSuggestions.Stations.ToArray();
            if (stationArray.Length > 0)
            {
                for (int i = 0; i < stationArray.Length - 1; ++i)
                {
                    Console.Write(stationArray[i] + ", ");
                }
                Console.Write(stationArray[stationArray.Length - 1]);
            }
            Console.WriteLine("");
            return userInput;
        }

        /// <summary>
        /// Page faults, static initializers, JIT, CPU cache, branch predictors, and context switches affect run time
        /// therefore performance is checked in initiaising multiple times, and seach performance checked by searching
        /// for multiple (randomly generated) user inputs
        /// </summary>
        public static void CheckPerformance()
        {
            Stopwatch stopwatch = new Stopwatch();

            IFileHandler fileHandler = new FileHandler("Data\\TrainStations.txt");
            ITrainStationSuggestorService trieService;
            ITrainStationSuggestorService listService;
            trieService = new TrieSuggestorService(fileHandler);
            listService = new ListSuggestorService(fileHandler);

            // Performance check for initialisation (building the stations lists
            int numOfInit = 100;
            stopwatch.Start();
            for (int i = 0; i < numOfInit; ++i)
            {
                trieService = new TrieSuggestorService(fileHandler);
            }
            stopwatch.Start();
            var trieInitPerf = stopwatch.ElapsedTicks / numOfInit;

            stopwatch.Reset();

            stopwatch.Start();
            for (int i = 0; i < 100; ++i)
            {
                listService = new ListSuggestorService(fileHandler);
            }
            stopwatch.Start();
            var listInitPerf = stopwatch.ElapsedTicks / numOfInit;
            stopwatch.Reset();

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Trie initialisation average performance: { trieInitPerf } ticks.");
            Console.WriteLine($"List initialisation average performance: { listInitPerf } ticks.");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Generating test user inputs for search performance test...");

            // Performance check for search
            Suggestions result;
            Random random = new Random();
            int numOfTestInput = 50;
            var userInputs = fileHandler.GetRandomStationPrefixes(numOfTestInput);
            Console.Write("Generated inputs: ");
            foreach (string input in userInputs)
            {
                Console.Write($"{input} ");
            }
            Console.WriteLine("");

            stopwatch.Start();
            foreach (string input in userInputs)
            {
                result = trieService.GetSuggestions(input);
            }
            stopwatch.Stop();
            var trieSearchPerf = stopwatch.ElapsedTicks / numOfTestInput;
            stopwatch.Reset();

            stopwatch.Start();
            foreach (string input in userInputs)
            {
                result = listService.GetSuggestions(input);
            }
            stopwatch.Stop();
            var listSearchPerf = stopwatch.ElapsedTicks / numOfTestInput;
            stopwatch.Reset();

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Trie search average performance: { trieSearchPerf } ticks.");
            Console.WriteLine($"List search average performance: { listSearchPerf } ticks.");
        }
    }
}
