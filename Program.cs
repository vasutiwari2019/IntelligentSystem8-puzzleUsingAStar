using System;
using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    class Program
    {
        // Program to demonstrate AStar
        static void Main(string[] args)
        {
            int[,] initialMatrix = { { 1, 2, 3 }, { 4, 8, 0 }, { 7, 6, 5 } };
            int[,] finalMatrix = { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 0 } };

            Matrix matrix = new Matrix(0, initialMatrix, initialMatrix);

            PriorityQueue<Matrix, int> priorityQueue = new PriorityQueue<Matrix, int>();

            priorityQueue.Enqueue(matrix, 0);

            Node node = new Node(0, 0, 0, priorityQueue);

            var heuristic = "h1";

            var count = 1000;

            AStarAlgo aStarAlgo = new AStarAlgo(initialMatrix, node, heuristic,count);

            aStarAlgo.ComputeAstar(0, aStarAlgo.InitialMatrix, finalMatrix, node);

            AStarAlgo.PrintMatrix(initialMatrix);

            if (aStarAlgo.LoopCount <= 0)
            {
                Console.WriteLine("No Solution");
                Environment.Exit(0);
            }

            AStarAlgo.PrintLinkedListMatrix(aStarAlgo.linkedListMatrix);
        }
    }
}
