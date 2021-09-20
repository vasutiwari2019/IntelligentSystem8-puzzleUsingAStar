using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentSystem8_puzzleUsingAStar
{
    public class AstarAlgo
    {
        public int[,] InitialMatrix { get; set; }


        public PriorityQueue<int[,], int> priorityQueue;

        public AstarAlgo(int[,] InitialMatrix)
        {
            this.InitialMatrix = InitialMatrix;
            priorityQueue = new PriorityQueue<int[,], int>();
        }

        public void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" {0}",matrix[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("*********");
        }
        // Need to do
        public void GetManhattanDistance(int[,] matrix)
        {
            int dist = 0;
            int x = 0, y = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    
                }
                Console.WriteLine();
            }
        }

        public int CalculateCost(int[,] tempMatrix, int [,] finalMatrix)
        {
            int dist = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tempMatrix[i, j] != finalMatrix[i, j] && tempMatrix[i,j] !=0)
                        dist++;
                }
            }

            return dist;
        }

        public void ComputeAstar(int f, int g, int h, int[,] initialMatrix, int[,] finalMatrix)
        {
            if (!CompareMatrices(initialMatrix, finalMatrix))
                {
                PrintMatrix(initialMatrix);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (initialMatrix[i, j] == 0)
                        {
                            ExpandNeighbours(initialMatrix, i, j, priorityQueue, g, h, f, finalMatrix);
                            ComputeAstar(f, g + 1, h, priorityQueue.Dequeue(), finalMatrix);
                        }
                    }
                }
            }
        }

        public void swap(int i, int j, int x, int y, int[,] tempMatrix)
        {
            int temp = tempMatrix[i,j];
            tempMatrix[i, j] = tempMatrix[x, y];
            tempMatrix[x, y] = temp;
        }

        public void ExpandNeighbours(int[,] initialMatrix,int i, int j, PriorityQueue<int[,],
                                     int> priorityQueue, int g, int h, int f, int[,] finalMatrix)
        {
            int[,] tempMatrix = (int[,])initialMatrix.Clone();

            if (i == 0)
            {
                if( j == 0)
                {
                    swap(i, j, 0, 1,tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else if( j == 1)
                {
                    swap(i, j, 0, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 0, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else
                {
                    swap(i, j, 0, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }
            }

            else if( i == 1)
            {
                if (j == 0)
                {
                    swap(i, j, 0, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else if(j == 1)
                {
                    swap(i, j, 0, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else
                {
                    swap(i, j, 0, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 1, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }
            }

            else
            {
                if (j == 0)
                {
                    swap(i, j, 1, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else if(j == 1)
                {
                    swap(i, j, 1, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 0, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);


                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
                }

                else
                {
                    swap(i, j, 1, 2, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);

                    tempMatrix = (int[,])initialMatrix.Clone();
                    swap(i, j, 2, 1, tempMatrix);
                    h = CalculateCost(tempMatrix, finalMatrix);
                    f = g + h;
                    priorityQueue.Enqueue(tempMatrix, f);
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
            PrintMatrix(initialMatrix);
            return true;
        }
    }
}
