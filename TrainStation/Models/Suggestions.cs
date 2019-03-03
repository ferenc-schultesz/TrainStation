using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Models
{
    public class Suggestions
    {
        public List<char> NextLetters { get; set; }
        public List<string> Stations { get; set; }

        public Suggestions()
        {
            NextLetters = new List<char>();
            Stations = new List<string>();
        }
    }
}
