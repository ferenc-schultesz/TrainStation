using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Utils
{
    public interface IFileHandler
    {
        List<string> ReadTextFileLines(string path);
        List<string> ReadTextFileCommaSeparated(string path);

    }
}
