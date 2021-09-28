using System;
using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    public class AStarAlgo
    {
        #region Properties
        public int[,] InitialMatrix { get; set; } // Property to initialize Initial Matrix.

        public Node Node { get; set; } // Property of type Node, Node will have the priority queue and the corresponding nodes g,f and h values.

        public LinkedList<int[,]> linkedListMatrix; // A linkedlist to keep track of the correct order of path.

        public string Heuristic { get; set; } // Type of heuristic used, h1 for misplaced tiles and h2 for manhattan distance.

        public int LoopCount { get; set; } // LoopCount so that 8 puzzle doesn't run into infinite steps.

        #endregion

        #region Constructor
        public AStarAlgo(int[,] InitialMatrix, Node Node, string Heuristic, int LoopCount)
        {
            this.InitialMatrix = InitialMatrix;
            this.Node = Node;
            linkedListMatrix = new LinkedList<int[,]>();
            this.Heuristic = Heuristic;
            this.LoopCount = LoopCount;
        }

        #endregion

        #region Public Methods
        // Method to print the matrix
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" {0}", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("*********");
        }

        // Method to find the matrix index based on it's values. Used to find index for Manhattan distance calculation.
        public static (int, int) FindMatrixIndex(int[,] finalMatrix, int element)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (element == finalMatrix[i, j])
                        return new(i, j);
                }
            }

            return default;
        }

        // Method for calculation heuristic distance h1 is Misplaced Tiles and h2 is Manhattan Distance.
        public int CalculateCost(int[,] tempMatrix, int[,] finalMatrix)
        {
            int dist = 0;
            switch (Heuristic)
            {
                case "h1":
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (tempMatrix[i, j] != finalMatrix[i, j] && tempMatrix[i, j] != 0)
                                dist++;
                        }
                    }

                    return dist;

                case "h2":
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                int x;
                                int y;
                                (x, y) = FindMatrixIndex(finalMatrix, tempMatrix[i, j]);

                                dist += Math.Abs(x - i) + Math.Abs(y - j);
                            }
                        }

                        return dist;
                    }
            }

            return 0;
        }

        // Method to implement the A* Algorithm. Recursive calls are there based on the least f values.
        public void ComputeAstar(int g, int[,] initialMatrix, int[,] finalMatrix, Node node)
        {
            LoopCount--;

            if (!CompareMatrices(initialMatrix, finalMatrix) && LoopCount > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (initialMatrix[i, j] == 0)
                        {
                            ExpandNeighbours(initialMatrix, i, j, g, finalMatrix, node.PriorityQueue.Peek().PreviousMatrix);

                            if (node.PriorityQueue.Peek().InitialMatrix == initialMatrix)
                                node.PriorityQueue.Dequeue();

                            linkedListMatrix.AddLast(node.PriorityQueue.Peek().InitialMatrix);

                            var initialTempMatrix = node.PriorityQueue.Peek().InitialMatrix;

                            node.PriorityQueue.Dequeue();

                            ComputeAstar(node.PriorityQueue.Peek().g + 1, initialTempMatrix, finalMatrix, node);
                        }
                    }
                }
            }
        }

        // Method to swap matrix elements, used in calculating child nodes.
        public static void Swap(int i, int j, int x, int y, int[,] tempMatrix)
        {
            int temp = tempMatrix[i, j];
            tempMatrix[i, j] = tempMatrix[x, y];
            tempMatrix[x, y] = temp;
        }

        // Method for calculating all possible combinations of moving the matrix, i.e all possible combinations of moving the tiles and storing them into the fringe.
        public void ExpandNeighbours(int[,] initialMatrix, int i, int j, int g, int[,] finalMatrix, int[,] previousMatrix)
        {
            int h, f;
            int[,] tempMatrix = (int[,])initialMatrix.Clone();

            var matrix = new Matrix(g, tempMatrix, previousMatrix);

            //var node = new Node(g, f, h);

            if (i == 0)
            {
                if (j == 0)
                {
                    Swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 0, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
            }
            else if (i == 1)
            {
                if (j == 0)
                {
                    Swap(i, j, 0, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
            }
            else
            {
                if (j == 0)
                {
                    Swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.PriorityQueue.Enqueue(matrix, f);
                }
            }
        }

        // Method to compare the two matrices.
        public static bool CompareMatrices(int[,] initialMatrix, int[,] finalMatrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (initialMatrix[i, j] != finalMatrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Method to print the LinkedList Matrix.
        public static void PrintLinkedListMatrix(LinkedList<int[,]> linkedListMatrix)
        {
            foreach (var item in linkedListMatrix)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(" {0}", item[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("*********");
            }
        }
        #endregion
    }
}
