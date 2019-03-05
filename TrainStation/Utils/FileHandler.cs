using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainStation.Utils
{
    /// <summary>
    /// File handler to read data files
    /// </summary>
    public class FileHandler : IFileHandler
    {
        private string dataFilePath;

        /// <summary>
        /// Constractor that take the path to the data file
        /// </summary>
        /// <param name="path">Data file path</param>
        public FileHandler(string path)
        {
            this.dataFilePath = path;
        }

        /// <summary>
        /// Reads the data file by lines and puts each line in to a list of strings
        /// </summary>
        /// /// <exception cref="System.Exception">Thrown when file not found by the given path
        /// <returns>List of strings where each element is a line from the data file</returns>
        public List<string> ReadTextFileLines()
        {
            if(!File.Exists(dataFilePath))
            {
                throw new Exception($"File not found at {dataFilePath}");
            }

            var lines = new List<string>();
            using (StreamReader stream = new StreamReader(dataFilePath))
            {
                while (!stream.EndOfStream)
                {
                    lines.Add(stream.ReadLine().ToUpper());
                }
            }
            return lines;
        }

        /// <summary>
        /// Reads the comma separated data file puts element in to a list of strings
        /// </summary>
        /// <exception cref="System.Exception">Thrown when file not found by the given path
        /// <returns>List of strings where each element is a comma separated calue from the data file</returns>
        public List<string> ReadTextFileCommaSeparated()
        {
            if (!File.Exists(dataFilePath))
            {
                throw new Exception($"File not found at {dataFilePath}");
            }

            List<string> words = new List<string>();
            string text = File.ReadAllText(dataFilePath);
            string[] splitted = text.Split(',');
            foreach(string station in splitted)
            {
                words.Add(station);
            }
            return words;
        }

        /// <summary>
        /// Generates a list of words bases on the datafiles. Only returns the first two letters of the randomly selected words.
        /// </summary>
        /// <param name="numOfStations">Number of station prefixes to be returned</param>
        /// <returns>List of randomly selected station prefixes</returns>
        public List<string> GetRandomStationPrefixes(int numOfStations)
        {
            string[] allStations = ReadTextFileLines().ToArray();
            Random random = new Random();
            string[] result = new string[numOfStations];

            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = allStations[random.Next(0, allStations.Length - 1)].Substring(0,2);
            }

            return result.ToList();
        }
    }
}
