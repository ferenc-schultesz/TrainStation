using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainStation.Utils
{
    public class FileHandler : IFileHandler
    {
        public FileHandler()
        {
        }

        public List<string> ReadTextFileLines(string path)
        {
            if(!File.Exists(path))
            {
                throw new Exception($"File not found at {path}");
            }

            var lines = new List<string>();
            using (StreamReader stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    lines.Add(stream.ReadLine().ToUpper());
                }
            }
            return lines;
        }

        public List<string> ReadTextFileCommaSeparated(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception($"File not found at {path}");
            }

            List<string> words = new List<string>();
            string text = File.ReadAllText(path);
            string[] splitted = text.Split(',');
            foreach(string station in splitted)
            {
                words.Add(station);
            }
            return words;
        }

    }
}
