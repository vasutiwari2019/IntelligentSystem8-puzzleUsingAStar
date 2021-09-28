namespace IntelligentSystem8_puzzleUsingAStar
{
    // A matrix class having initial and previous matrix and corresponding g values.
    public class Matrix
    {
        #region Properties
        public int[,] InitialMatrix { get; set; } // prop to store initial matrix

        public int[,] PreviousMatrix { get; set; } // prop to store final matrix
        public int g { get; set; } // prop to store corresponding g value
        #endregion

        #region Constructor
        public Matrix(int g, int[,] InitialMatrix, int[,] PreviousMatrix)
        {
            this.g = g;
            this.InitialMatrix = InitialMatrix;
            this.PreviousMatrix = PreviousMatrix;
        }
        #endregion
    }
}
