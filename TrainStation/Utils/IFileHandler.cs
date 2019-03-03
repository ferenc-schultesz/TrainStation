using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Utils
{
    public interface IFileHandler
    {
        List<string> ReadTextFileLines(string path);
        List<string> ReadTextFileCommaSeparated(string path);
        void ReadTextFileLinesRef(string path, ref List<string> list);
    }
}
