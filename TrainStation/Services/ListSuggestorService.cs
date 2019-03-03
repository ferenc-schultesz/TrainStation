using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainStation.Models;
using TrainStation.Utils;

namespace TrainStation.Services
{
    public class ListSuggestorService : ITrainStationSuggestorService
    {
        private List<string> stations;
        private IFileHandler fileHandler;
        public ListSuggestorService(string dataFilePath, IFileHandler _fileHandler)
        {
            this.fileHandler = _fileHandler;
            this.stations = fileHandler.ReadTextFileLines(dataFilePath);
        }

        public Suggestions GetSuggestions(string userInput)
        {
            userInput = userInput.ToUpper();
            var suggestions = new Suggestions();

            // Get all stations
            if (string.IsNullOrEmpty(userInput))
            {
                suggestions.Stations = this.stations;
            }
            else
            {
                suggestions.Stations = this.stations.Where(s => s.ToLower().StartsWith(userInput.ToLower())).ToList();
            }

            // Get possible next chars
            List<char> chars = new List<char>();
            foreach (string station in suggestions.Stations)
            {
                string sub = station.Substring(userInput.Length);
                if (sub.Length >0)
                {
                    chars.Add(sub[0]);
                }
            }
            
            suggestions.NextLetters = chars.Distinct().ToList();
            return suggestions;
        }
    }
}
