using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainStation.Utils
{
    public class FileHandler
    {
        public static List<string> ReadTextFileLines(string path)
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
                    lines.Add(stream.ReadLine());
                }
            }
            return lines;
        }
    }
}
