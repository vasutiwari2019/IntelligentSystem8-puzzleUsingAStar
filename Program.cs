using System;
using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    class Program
    {
        // Program to demonstrate AStar
        static void Main(string[] args)
        {
            //int[,] initialMatrix = { { 1, 2, 3 }, { 4, 8, 0, }, { 7, 6, 5 } };
            //int[,] finalMatrix = { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 0 } };

            //AstarAlgo astarAlgo = new AstarAlgo(initialMatrix);

            //astarAlgo.ComputeAstar(0, 0, 0, astarAlgo.InitialMatrix, finalMatrix);

            ////astarAlgo.PrintLinkedListMatrix(astarAlgo.linkedListMatrix);

            ////Console.WriteLine("Misplaced items {0}", astarAlgo.CalculateCost(astarAlgo.InitialMatrix, finalMatrix));
            ///

            int[,] initialMatrix = { { 1, 2, 3 }, { 5, 6, 0 }, { 7, 8, 4 } };
            int[,] finalMatrix = { { 1, 2, 3 }, { 5, 8, 6, }, { 0, 7, 4 } };

            Matrix matrix = new Matrix(0, initialMatrix, initialMatrix);

            PriorityQueue<Matrix, int> priorityQueue = new PriorityQueue<Matrix, int>();

            priorityQueue.Enqueue(matrix, 0);
            Node node = new Node(0, 0, 0, priorityQueue);

            AStarAlgoCopy aStarAlgoCopy = new AStarAlgoCopy(initialMatrix, node);

            aStarAlgoCopy.linkedListMatrix.AddFirst(initialMatrix);

            aStarAlgoCopy.ComputeAstar(0, 0, 0, aStarAlgoCopy.InitialMatrix, finalMatrix, node, initialMatrix);

            aStarAlgoCopy.PrintLinkedListMatrix(aStarAlgoCopy.linkedListMatrix);

            //Console.WriteLine("Misplaced items {0}", astarAlgo.CalculateCost(astarAlgo.InitialMatrix, finalMatrix));
        }
    }
}
