using TrainStation.Models;

namespace TrainStation.Services
{
    interface ITrainStationSuggestorService
    {
        Suggestions GetSuggestions(string userInput);
    }
}
