using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Models
{
    /// <summary>
    /// Suggestions class include the next letters and possible stations depending on the current user input
    /// </summary>
    public class Suggestions
    {
        public List<char> NextLetters { get; set; }
        public List<string> Stations { get; set; }

        /// <summary>
        /// Constructor to init Suggestions
        /// </summary>
        public Suggestions()
        {
            NextLetters = new List<char>();
            Stations = new List<string>();
        }
    }
}
