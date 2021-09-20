using System;

namespace IntelligentSystem8_puzzleUsingAStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] initialMatrix = { { 1, 2, 3 }, { 4, 8, 0, }, { 7, 6, 5 } };
            int[,] finalMatrix = { { 1, 2, 3 }, { 4, 5, 6, }, { 7, 8, 0 } };

            AstarAlgo astarAlgo = new AstarAlgo(initialMatrix);

            astarAlgo.ComputeAstar(0, 0, 0, astarAlgo.InitialMatrix, finalMatrix);

            Console.WriteLine("Misplaced items {0}", astarAlgo.CalculateCost(astarAlgo.InitialMatrix, finalMatrix));
        }
    }
}
