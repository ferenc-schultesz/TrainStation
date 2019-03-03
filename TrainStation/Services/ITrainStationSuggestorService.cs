using System;
using System.Collections.Generic;
using System.Text;
using TrainStation.Models;

namespace TrainStation.Services
{
    interface ITrainStationSuggestorService
    {
        Suggestions GetSuggestions(string userInput);
    }
}
