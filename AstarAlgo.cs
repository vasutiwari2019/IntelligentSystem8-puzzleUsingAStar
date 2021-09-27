using System;
using System.Collections.Generic;

namespace IntelligentSystem8_puzzleUsingAStar
{
    public class AStarAlgo
    {
        public int[,] InitialMatrix { get; set; }

        public Node Node { get; set; }

        public LinkedList<int[,]> linkedListMatrix;

        public string Heuristic { get; set; }

        public int LoopCount { get; set; }

        public AStarAlgo(int[,] InitialMatrix, Node Node, string Heuristic, int LoopCount)
        {
            this.InitialMatrix = InitialMatrix;
            this.Node = Node;
            linkedListMatrix = new LinkedList<int[,]>();
            this.Heuristic = Heuristic;
            this.LoopCount = LoopCount;
        }

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
                            ExpandNeighbours(initialMatrix, i, j, g, finalMatrix, node.priorityQueue.Peek().PreviousMatrix);

                            if (node.priorityQueue.Peek().InitialMatrix == initialMatrix)
                                node.priorityQueue.Dequeue();

                            linkedListMatrix.AddLast(node.priorityQueue.Peek().InitialMatrix);

                            var initialTempMatrix = node.priorityQueue.Peek().InitialMatrix;

                            node.priorityQueue.Dequeue();

                            ComputeAstar(node.priorityQueue.Peek().g + 1, initialTempMatrix, finalMatrix, node);
                        }
                    }
                }
            }
        }

        public static void Swap(int i, int j, int x, int y, int[,] tempMatrix)
        {
            int temp = tempMatrix[i, j];
            tempMatrix[i, j] = tempMatrix[x, y];
            tempMatrix[x, y] = temp;
        }

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
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 0, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
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
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
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
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else if (j == 1)
                {
                    Swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
                else
                {
                    Swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    Swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    Node.priorityQueue.Enqueue(matrix, f);
                }
            }
        }

        //Returns true if matrices are equal
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
    }
}
