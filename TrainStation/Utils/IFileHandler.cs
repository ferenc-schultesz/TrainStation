using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Utils
{
    public interface IFileHandler
    {
        List<string> ReadTextFileLines();

        List<string> ReadTextFileCommaSeparated();

        List<string> GetRandomStationPrefixes(int numOfStations);
    }
}
