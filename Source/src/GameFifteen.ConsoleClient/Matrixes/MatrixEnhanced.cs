namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Text;

    using GameFifteen.ConsoleClient.Interfaces;

    /// <summary>
    /// The proxy Matrix class
    /// </summary>
    internal class MatrixEnhanced : BasicMatrix, IPrintable
    {
        /// <summary>
        /// The inner matrix field
        /// </summary>
        private Matrix innerMatrix;

        /// <summary>
        /// Initializes a new instance of the MatrixEnhanced class
        /// </summary>
        public MatrixEnhanced()
            : base()
        {
            this.innerMatrix = new Matrix();
        }

        /// <summary>
        /// An indexer for the MatrixEnhanced c lass
        /// </summary>
        /// <param name="row">Represents the rows</param>
        /// <param name="column">Represents the columns</param>
        /// <returns>Returns an element of the matrix</returns>
        public override string this[int row, int column]
        {
            get
            {
                return this.innerMatrix[row, column];
            }

            set
            {
                this.innerMatrix[row, column] = value;
            }
        }

        /// <summary>
        /// Initializes the matrix
        /// </summary>
        public override void InitializeMatrix()
        {
            this.innerMatrix.InitializeMatrix();
            this.EmptyCells = this.innerMatrix.EmptyCells;
        }

        /// <summary>
        /// Shuffles the matrix
        /// </summary>
        public void ShuffleMatrix()
        {
            var random = new Random();
            int shuffles = random.Next(Constants.GameBoardSize, Constants.GameBoardSize * 10);
            for (int i = 0; i < shuffles; i++)
            {
                var directionLength = this.DirectionRow.Length;
                int direction = random.Next(directionLength);
                if (this.IsNextCellValid(direction))
                {
                    this.MoveCell(direction);
                }
            }

            if (this.AreNumbersSequential())
            {
                this.ShuffleMatrix();
            }
        }

        /// <summary>
        /// Checks if the numbers inside the matrix are sequential
        /// </summary>
        /// <returns>A boolean value</returns>
        public bool AreNumbersSequential()
        {
            var emptyCellRow = this.EmptyCells[0];
            var emptyCellColumn = this.EmptyCells[1];

            bool isEmptyCellInPlace = emptyCellRow == Constants.GameBoardRows - 1 && emptyCellColumn == Constants.GameBoardColumns - 1;
            if (!isEmptyCellInPlace)
            {
                return false;
            }

            int cellValue = 1;

            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns && cellValue < Constants.GameBoardSize; column++)
                {
                    if (this.innerMatrix[row, column] != cellValue.ToString())
                    {
                        return false;
                    }

                    cellValue++;
                }
            }

            return true;
        }

        /// <summary>
        /// Moves cells to a given direction
        /// </summary>
        /// <param name="direction">The direction</param>
        public void MoveCell(int direction)
        {
            var directions = GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];
            var emptyCellRow = this.EmptyCells[0];
            var emptyCellColumn = this.EmptyCells[1];

            this.innerMatrix[emptyCellRow, emptyCellColumn] = this.innerMatrix[nextCellRow, nextCellColumn];
            this.innerMatrix[nextCellRow, nextCellColumn] = Constants.EmptyCellValue;

            this.EmptyCells[0] = nextCellRow;
            this.EmptyCells[1] = nextCellColumn;
        }

        /// <summary>
        /// Implements the IPrintable interface
        /// </summary>
        /// <returns>A string which is ready to be printed</returns>
        public string ToPrintable()
        {
            StringBuilder matrixBuilder = new StringBuilder();
            var horizontalBorder = new StringBuilder("  ");
            for (int i = 0; i < Constants.GameBoardColumns; i++)
            {
                horizontalBorder.Append("---");
            }

            horizontalBorder.Append("- \n");
            matrixBuilder.Append(horizontalBorder);
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                matrixBuilder.Append(" |");
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    matrixBuilder.Append(string.Format("{0,3}", this.innerMatrix[row, column]));
                }

                matrixBuilder.Append(" |\n");
            }

            matrixBuilder.Append(horizontalBorder);
            return matrixBuilder.ToString();
        }
    }
}
