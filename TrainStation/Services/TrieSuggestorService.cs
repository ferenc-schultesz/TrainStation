using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainStation.Models;
using TrainStation.Utils;

namespace TrainStation.Services
{
    /// <summary>
    /// ITrainStationSuggestorService implementation by using Trie search tree as a data structure and Depth-first search all stations
    /// of a particular node
    /// </summary>
    public class TrieSuggestorService : ITrainStationSuggestorService
    {
        private readonly Node root;
        private readonly IFileHandler fileHandler;

        /// <summary>
        /// Constructor takes an IFileHandler and creates the prefix tree
        /// </summary>
        /// <param name="_fileHandler">Object that implements the IFileHandler interface to deal with the input data file.</param>
        public TrieSuggestorService(IFileHandler _fileHandler)
        {
            root = new Node()
            {
                parent = null,
                prefix = "",
                children = new List<Node>(),
                value = '^'
            };

            this.fileHandler = _fileHandler;
            List<string> stations = fileHandler.ReadTextFileLines();
            for (int i = 0; i < stations.Count; i++)
            {
                this.Insert(stations[i]);
            }

        }

        /// <summary>
        /// Gets the suggestions based on the user input
        /// </summary>
        /// <param name="userInput">User input</param>
        /// <returns>Suggestions including possible next letters and stations.</returns>
        public Suggestions GetSuggestions(string userInput)
        {
            userInput = userInput.ToUpper();
            Suggestions suggestions = new Suggestions();

            // get stations
            Node nodeWithUSerInputPrefix = SearchNodeByPrefix(userInput.ToUpper());
            if (nodeWithUSerInputPrefix == null)
            {
                return suggestions;
            }
            suggestions.Stations = GetStationsByNode(nodeWithUSerInputPrefix);
            suggestions.Stations.Sort();

            // get next letters
            foreach (Node node in nodeWithUSerInputPrefix.children)
            {
                if (node.value != '*')
                {
                    suggestions.NextLetters.Add(node.value);
                }
            }

            suggestions.NextLetters = suggestions.NextLetters.Distinct().ToList();
            suggestions.NextLetters.Sort();

            return suggestions;
        }

        /// <summary>
        /// Searches the node where by the prefix. Search is done by characters in the prefix which gives
        /// O(m) time complexity where m is the length of the word
        /// </summary>
        /// <param name="prefix">User input</param>
        /// <returns name="node">Node with the given prefix</returns>
        private Node SearchNodeByPrefix(string prefix)
        {
            Node node = this.root;

            foreach (char c in prefix)
            {

                var child = node.FindChildNode(c);
                if (child == null)
                {
                    return null;
                }
                else
                {
                    node = child;
                }
            }
            return node;
        }

        /// <summary>
        /// Gets all possible stations from a node by using Depth-first search
        /// </summary>
        /// <param name="node">Trie node</param>
        /// <returns name="stations">All stations with the given node's prefix</returns>
        private List<string> GetStationsByNode (Node node)
        {
            string prefix = node.prefix;
            List<string> stations = new List<string>();
            if (node != null)
            {
                Stack<Node> stack = new Stack<Node>();
                stack.Push(node);
                do
                {
                    node = stack.Pop();
                    if (node.children.Count == 0 || node.value == '*')
                    {
                        stations.Add(node.prefix.Replace("*", ""));
                    }
                    foreach(Node n in node.children)
                    {
                        stack.Push(n);
                    }
                } while (stack.Count != 0);

            }
            return stations;
        }

        /// <summary>
        /// Inserts a new word to the Trie.
        /// </summary>
        /// <param name="s">The word</param>
        private void Insert(string s)
        {
            Node node = this.root;
            foreach (char c in s)
            {
                var child = node.FindChildNode(c);
                if (child == null)
                {
                    var newNode = new Node(c, node);
                    node.children.Add(newNode);
                    node = newNode;
                }
                else
                {
                    node = child;
                }
            }
            node.children.Add(new Node('*', node));
        }
    }
}
