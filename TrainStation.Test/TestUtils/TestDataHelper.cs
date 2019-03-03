using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainStation.Test.TestUtils
{
    public class TestDataHelper
    {
        public static void CreateTestFile(string fileName, List<string> content)
        {
            // Make sure file does not exist
            DeleteTestFile(fileName);

            // Create test data file
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (string line in content)
                {
                    sw.WriteLine(line);
                }
            }
        }

        public static void DeleteTestFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
