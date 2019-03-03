using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Models
{
    /// <summary>
    /// Node class for Trie data structure. Each node has a value, reference to all its children and parent, and sum of its parents values
    /// </summary>
    public class Node
    {
        public char value { get; set; }
        public List<Node> children { get; set; }
        public Node parent { get; set; }

        public string prefix { get; set; }

        /// <summary>
        /// Empty constructor is necessary for creating the root node.
        /// </summary>
        public Node()
        { }

        /// <summary>
        /// Constructor taking the value of the Node and reference to its parent.
        /// </summary>
        /// <param name="value">Value of the node</param>
        /// <param name="parent">Parent node</param>
        public Node(char value, Node parent)
        {
            this.value = value;
            this.children = new List<Node>();
            this.parent = parent;
            this.prefix = parent.prefix + value;
        }

        /// <summary>
        /// Finds the child of the node given a value
        /// </summary>
        /// <param name="c">Value of the child node that we are looking for.</param>
        /// <returns>Node with the value, or null if node not found</returns>
        public Node FindChildNode(char c)
        {
            foreach (var child in children)
            {
                if (child.value == c)
                {
                    return child;
                }
            }
            return null;
        }

    }
}
