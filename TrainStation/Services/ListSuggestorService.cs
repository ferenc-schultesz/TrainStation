using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainStation.Models;
using TrainStation.Utils;

namespace TrainStation.Services
{
    /// <summary>
    /// ITrainStationSuggestorService implekmentation by using List as datastructure to hold stations.
    /// </summary>
    public class ListSuggestorService : ITrainStationSuggestorService
    {
        private List<string> stations;
        private IFileHandler fileHandler;

        /// <summary>
        /// Constructor takes the data file path and a FileHandler and populates the stations list
        /// </summary>
        /// <param name="dataFilePath">Path of the data file</param>
        /// <param name="_fileHandler">Object that implements the IFileHandler interface to deal with the input data file.</param>
        public ListSuggestorService(string dataFilePath, IFileHandler _fileHandler)
        {
            this.fileHandler = _fileHandler;
            this.stations = fileHandler.ReadTextFileLines(dataFilePath);
        }

        /// <summary>
        /// Gets the suggestions based on the user input
        /// </summary>
        /// <param name="userInput">User input</param>
        /// <returns>Suggestions including possible next letters and stations.</returns>
        public Suggestions GetSuggestions(string userInput)
        {
            userInput = userInput.ToUpper();
            var suggestions = new Suggestions();

            // Get all stations from stations list that start with the user input
            // If the user input is empty, get all stations
            if (string.IsNullOrEmpty(userInput))
            {
                suggestions.Stations = this.stations;
            }
            else
            {
                suggestions.Stations = this.stations.Where(s => s.ToLower().StartsWith(userInput.ToLower())).ToList();
            }
            
            // Get all possible next letters
            // Loop through on all suggested stations
            foreach (string station in suggestions.Stations)
            {
                string sub = station.Substring(userInput.Length);
                // If there are more letters after taking the prefix out of the station
                if (sub.Length > 0)
                {
                    // Add the next letter (char) to the suggested next letters
                    suggestions.NextLetters.Add(sub[0]);
                }
            }
            
            // Only need distinct characters
            suggestions.NextLetters = suggestions.NextLetters.Distinct().ToList();
            // Order the list
            suggestions.NextLetters.Sort();
            return suggestions;
        }
    }
}
