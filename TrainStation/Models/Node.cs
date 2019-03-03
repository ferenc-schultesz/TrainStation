using System;
using System.Collections.Generic;
using System.Text;

namespace TrainStation.Models
{
    public class Node
    {
        public char value { get; set; }
        public List<Node> children { get; set; }
        public Node parent { get; set; }

        public string prefix { get; set; }

        public Node()
        { }
        public Node(char value, Node parent)
        {
            this.value = value;
            this.children = new List<Node>();
            this.parent = parent;
            this.prefix = parent.prefix + value;
        }

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
