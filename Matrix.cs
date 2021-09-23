namespace IntelligentSystem8_puzzleUsingAStar
{
    public class Matrix
    {
        public int[,] InitialMatrix { get; set; }

        public int[,] PreviousMatrix { get; set; }
        public int g { get; set; }

        public Matrix(int g, int[,] InitialMatrix, int[,] PreviousMatrix)
        {
            this.g = g;
            this.InitialMatrix = InitialMatrix;
            this.PreviousMatrix = PreviousMatrix;
        }
    }
}
