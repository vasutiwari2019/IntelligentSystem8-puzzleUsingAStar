using System;
using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    internal static class Program
    {
        // Program to demonstrate AStar
        static void Main(string[] args)
        {
            int[,] initialMatrix = { { 0, 1, 3 }, { 4, 2, 5 }, { 7, 8, 6 } }; // Initialized initial matrix to default value
            int[,] finalMatrix = { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 0 } }; // Initialized final matrix to default value

            string heuristic = "h2";

            Console.WriteLine("Enter the initial matrix in array format separated by spaces, eg  1 2 3 4 5 6 7 8 0, please consider 0 for space.");

            string str = Console.ReadLine();

            string[] strK = str.Split(" ");
            int countStr = 0;
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        initialMatrix[i, j] = Convert.ToInt32(strK[countStr]);
                        countStr++;
                    }
                }
                countStr = 0;
                Console.WriteLine("Enter the goal matrix in array format separated by spaces, eg  1 2 3 4 5 6 7 8 0, please consider 0 for space.");

                str=Console.ReadLine();

                strK = str.Split(" ");

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        finalMatrix[i, j] = Convert.ToInt32(strK[countStr]);
                        countStr++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Input not in correct format {0}",e);

                Environment.Exit(1);
            }

            Console.WriteLine("Choose 1 for Misplaced tiles and 2 for Manhattan Distance");

            var input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    heuristic = "h1";
                   break;

                case "2":

                default:
                    heuristic = "h2";
                    break;
            }

            Matrix matrix = new Matrix(0, initialMatrix, initialMatrix);

            PriorityQueue<Matrix, int> priorityQueue = new PriorityQueue<Matrix, int>();

            priorityQueue.Enqueue(matrix, 0);

            Node node = new Node(0, 0, 0, priorityQueue);

            const int count = 1000;

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
