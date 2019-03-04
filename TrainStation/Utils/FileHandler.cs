using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainStation.Utils
{
    public class FileHandler : IFileHandler
    {
        private string dataFilePath;

        public FileHandler(string path)
        {
            this.dataFilePath = path;
        }

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
