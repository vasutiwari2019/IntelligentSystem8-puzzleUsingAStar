using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentSystem8_puzzleUsingAStar
{
    public class AStarAlgoCopy
    {
        public int[,] InitialMatrix { get; set; }

        public Node node { get; set; }

        public LinkedList<int[,]> linkedListMatrix;

        public string Heuristic { get; set; }

        public AStarAlgoCopy(int[,] InitialMatrix, Node node, string Heuristic)
        {
            this.InitialMatrix = InitialMatrix;
            this.node = node;
            linkedListMatrix = new LinkedList<int[,]>();
            this.Heuristic = Heuristic;
        }

        public void PrintMatrix(int[,] matrix)
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

        public static (int,int) FindMatrixIndex(int[,] finalMatrix, int element)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(element == finalMatrix[i,j])
                        return new (i,j);
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

        public void ComputeAstar(int f, int g, int h, int[,] initialMatrix, int[,] finalMatrix, Node node, int[,] previousMatrix)
        {
            if (!CompareMatrices(initialMatrix, finalMatrix))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (initialMatrix[i, j] == 0)
                        {
                            ExpandNeighbours(initialMatrix, i, j, g, h, f, finalMatrix, node.priorityQueue.Peek().PreviousMatrix);
                            
                            if (node.priorityQueue.Peek().InitialMatrix == initialMatrix)
                                node.priorityQueue.Dequeue();

                            if (CompareMatrices(node.priorityQueue.Peek().PreviousMatrix, linkedListMatrix.Last.Value)) //linkedListMatrix.Last.Value
                            {
                                linkedListMatrix.AddLast(node.priorityQueue.Peek().InitialMatrix);
                            }
                            else
                            {
                                
                                linkedListMatrix.AddLast(node.priorityQueue.Peek().InitialMatrix);
                            }
                            var initialTempMatrix = node.priorityQueue.Peek().InitialMatrix;

                            var previousTempMatrix = node.priorityQueue.Peek().PreviousMatrix;
                            node.priorityQueue.Dequeue();

                            ComputeAstar(node.f, node.priorityQueue.Peek().g + 1, node.h, initialTempMatrix, finalMatrix, node, previousTempMatrix);
                            //linkedListMatrix.AddLast(priorityQueue.Peek());
                            //ComputeAstar(f, g + 1, h, priorityQueue.Dequeue(), finalMatrix);
                            //}


                            //else
                            //{
                            //    //linkedListMatrix.RemoveLast();
                            //    node.priorityQueue.Dequeue();
                            //    ComputeAstar(node.f, node.priorityQueue.Peek().g + 1, node.h, node.priorityQueue.Dequeue().InitialMatrix, finalMatrix, node);
                            //    //RemoveItemfromLinkedList(linkedListMatrix, priorityQueue.Peek());
                            //}
                            //node.priorityQueue.Dequeue();
                        }

                    }
                }
            }
                //PrintMatrix(initialMatrix);
            
            //PrintMatrix(node.priorityQueue.Peek());
            //PrintLinkedListMatrix(linkedListMatrix);
        }

        public void swap(int i, int j, int x, int y, int[,] tempMatrix)
        {
            int temp = tempMatrix[i, j];
            tempMatrix[i, j] = tempMatrix[x, y];
            tempMatrix[x, y] = temp;
        }

        public void ExpandNeighbours(int[,] initialMatrix, int i, int j, int g, int h, int f, int[,] finalMatrix, int[,] previousMatrix)
        {
            int[,] tempMatrix = (int[,])initialMatrix.Clone();

            var matrix = new Matrix(g, tempMatrix, previousMatrix);

            //var node = new Node(g, f, h);

            if (i == 0)
            {
                if (j == 0)
                {
                    swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else if (j == 1)
                {
                    swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 0, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else
                {
                    swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }
            }

            else if (i == 1)
            {
                if (j == 0)
                {
                    swap(i, j, 0, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else if (j == 1)
                {
                    swap(i, j, 0, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else
                {
                    swap(i, j, 0, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }
            }

            else
            {
                if (j == 0)
                {
                    swap(i, j, 1, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else if (j == 1)
                {
                    swap(i, j, 1, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 0, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }

                else
                {
                    swap(i, j, 1, 2, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    matrix = new Matrix(g, tempMatrix, previousMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    matrix.g = g;
                    node.priorityQueue.Enqueue(matrix, f);
                }
            }
        }

        //Returns true if matrices are equal
        public bool CompareMatrices(int[,] initialMatrix, int[,] finalMatrix)
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
            //PrintMatrix(initialMatrix);
            return true;
        }

        public void PrintLinkedListMatrix(LinkedList<int[,]> linkedListMatrix)
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

        public void RemoveItemfromLinkedList(List<int[,]> linkedListMatrix, int[,] tempMat)
        {
            bool result;
            var listMatrix = new List<int[,]>();
            foreach (var item in linkedListMatrix)
            {
                result = CompareMatrices(tempMat, item);
                if (result)
                    listMatrix.Add(item);
            }

            foreach (var item in listMatrix)
            {
                linkedListMatrix.Remove(item);
            }
        }
    }
}
