namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Collections;
    using System.Text;

    public class Matrix : IPrintable, IEnumerable
    {
        private readonly int[] DirectionRow = { -1, 0, 1, 0 };
        private readonly int[] DirectionColumn = { 0, 1, 0, -1 };

        private int turn;

        private int emptyCellRow;
        private int emptyCellColumn;

        private string[,] matrix;
        public Matrix()
        {
            InitializeMatrix();
            this.turn = 0;
        }
        private void InitializeMatrix()
        {
            this.matrix = new string[Constants.GameBoardRows, Constants.GameBoardColumns];

            int cellValue = 1;
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    this.matrix[row, column] = cellValue.ToString();

                    cellValue++;
                }
            }

            this.emptyCellRow = Constants.GameBoardRows - 1;
            this.emptyCellColumn = Constants.GameBoardColumns - 1;

            this.matrix[this.emptyCellRow, this.emptyCellColumn] = Constants.EmptyCellValue;
        }
        private void ShuffleMatrix()
        {
            var random = new Random();
            int shuffles = random.Next(Constants.GameBoardSize, Constants.GameBoardSize * 100);
            for (int i = 0; i < shuffles; i++)
            {
                var directionLength = this.DirectionRow.Length;
                int direction = random.Next(directionLength);
                if (IsNextCellValid(direction))
                {
                    MoveCell(direction);
                }
            }

            if (AreNumbersSequential())
            {
                ShuffleMatrix();
            }
        }

        private bool AreNumbersSequential()
        {
            // TODO: Can be extracted to method
            bool isEmptyCellInPlace = this.emptyCellRow == Constants.GameBoardRows - 1 && this.emptyCellColumn == Constants.GameBoardColumns - 1;
            if (!isEmptyCellInPlace)
            {
                return false;
            }

            int cellValue = 1;

            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns && cellValue < Constants.GameBoardSize; column++)
                {
                    if (this.matrix[row, column] != cellValue.ToString())
                    {
                        return false;
                    }

                    cellValue++;
                }
            }

            return true;
        }


        private bool IsNextCellValid(int direction)
        {
            var directions = GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;

            return isRowValid && isColumnValid;
        }

        private void MoveCell(int direction)
        {
            var directions = GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];

            this.matrix[this.emptyCellRow, this.emptyCellColumn] = this.matrix[nextCellRow, nextCellColumn];
            this.matrix[nextCellRow, nextCellColumn] = Constants.EmptyCellValue;

            this.emptyCellRow = nextCellRow;
            this.emptyCellColumn = nextCellColumn;

            turn++;
        }

        private int[] GetDirections(int direction)
        {
            int nextCellRow = this.emptyCellRow + DirectionRow[direction];
            int nextCellColumn = this.emptyCellColumn + DirectionColumn[direction];
            return new int[] { nextCellRow, nextCellColumn };
        }

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
                    matrixBuilder.Append(String.Format("{0,3}", this.matrix[row, column]));
                }

                matrixBuilder.Append(" |\n");
            }
            matrixBuilder.Append(horizontalBorder);
            return matrixBuilder.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    yield return this.matrix[row, column];
                }
            }
        }
    }
}
