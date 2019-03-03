using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainStation.Models;
using TrainStation.Utils;

namespace TrainStation.Services
{
    public class TrieSuggestorService : ITrainStationSuggestorService
    {
        private readonly Node root;
        private readonly IFileHandler fileHandler;
        public TrieSuggestorService(string dataFilePath, IFileHandler _fileHandler)
        {
            root = new Node()
            {
                parent = null,
                prefix = "",
                children = new List<Node>(),
                value = '^'
            };

            this.fileHandler = _fileHandler;
            List<string> stations = fileHandler.ReadTextFileLines(dataFilePath);
            for (int i = 0; i < stations.Count; i++)
            {
                this.Insert(stations[i]);
            }

        }
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
