using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    public class Node
    {
        public PriorityQueue<Matrix, int> priorityQueue { get; set; }
        public int g { get; set; }
        public int f { get; set; }

        public int h { get; set; }
        public Node(int g, int f, int h, PriorityQueue<Matrix, int> priorityQueue)
        {
            this.g = g;
            this.f = f;
            this.h = h;
            this.priorityQueue = priorityQueue;
        }
    }
}
