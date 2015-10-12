namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Collections;

    /// <summary>
    /// The first implementation of the BasicMatrix class
    /// </summary>
    internal class Matrix : BasicMatrix, IEnumerable
    {
        /// <summary>
        /// Initializes a new instance of the Matrix class
        /// </summary>
        public Matrix()
            : base()
        {
        }

        /// <summary>
        /// And indexer for the matrix
        /// </summary>
        /// <param name="row">Represents the rows</param>
        /// <param name="column">Represents the columns</param>
        /// <returns>An element of the matrix</returns>
        public override string this[int row, int column]
        {
            get
            {
                return this.Matrix[row, column];
            }

            set
            {
                this.Matrix[row, column] = value;
            }
        }

        /// <summary>
        /// Initializes the matrix
        /// </summary>
        public override void InitializeMatrix()
        {
            this.Matrix = new string[Constants.GameBoardRows, Constants.GameBoardColumns];

            int cellValue = 1;
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    this.Matrix[row, column] = cellValue.ToString();

                    cellValue++;
                }
            }

            var emptyCellRow = Constants.GameBoardRows - 1;
            var emptyCellColumn = Constants.GameBoardColumns - 1;

            this.EmptyCells[0] = emptyCellRow;
            this.EmptyCells[1] = emptyCellColumn;
            this.Matrix[emptyCellRow, emptyCellColumn] = Constants.EmptyCellValue;
        }
    }
}
