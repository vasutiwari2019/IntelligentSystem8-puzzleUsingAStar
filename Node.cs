using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    // Node class to store the node, a node will have corresponding f g and h values.
    public class Node
    {
        #region Property
        public PriorityQueue<Matrix, int> PriorityQueue { get; set; } // prop for priority queue to maintain original expansion order.
        public int G { get; set; } // prop to store corresponding g value
        public int F { get; set; } // prop to store corresponding f value

        public int H { get; set; } // prop to store corresponding h value
        #endregion

        #region Constructor
        public Node(int g, int f, int h, PriorityQueue<Matrix, int> priorityQueue)
        {
            this.G = g;
            this.F = f;
            this.H = h;
            this.PriorityQueue = priorityQueue;
        }
        #endregion
    }
}
