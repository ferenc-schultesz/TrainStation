using TrainStation.Models;

namespace TrainStation.Services
{
    public interface ITrainStationSuggestorService
    {
        Suggestions GetSuggestions(string userInput);
    }
}
