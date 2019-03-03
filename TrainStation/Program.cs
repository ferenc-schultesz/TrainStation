using System;
using TrainStation.Services;

namespace TrainStation
{
    public class Program
    {
        static void Main(string[] args)
        {
            ITrainStationSuggestorService suggestor;
            try
            {
                suggestor = new ListSuggestorService("Data\\TrainStations.txt");
                Console.WriteLine("Program initialised.");
                string searchString;
                Console.Write("Enter full or partial station name: ");
                searchString = Console.ReadLine();

                var suggestions = suggestor.GetSuggestions(searchString);

                Console.WriteLine("------------------------------------");
                Console.Write("Possible next letters: ");
                foreach(char letter in suggestions.NextLetters)
                {
                    Console.Write(letter + ", ");
                }

                Console.WriteLine("------------------------------------");
                Console.Write("Possible Stations: ");
                foreach (string station in suggestions.Stations)
                {
                    Console.Write(station + ", ");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}
