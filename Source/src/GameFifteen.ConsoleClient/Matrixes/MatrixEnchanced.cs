namespace GameFifteen.ConsoleClient
{
    using System;

    public class MatrixEnchanced : BasicMatrix
    {
        private int turn;
        private Matrix innterMatrix;
        public MatrixEnchanced()
        {
            this.innterMatrix = new Matrix();
        }

        public override void InitializeMatrix()
        {
            this.innterMatrix.InitializeMatrix();
        }
        public void ShuffleMatrix()
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

        public bool AreNumbersSequential()
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

        public void MoveCell(int direction)
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

        public override string this[int row, int column]
        {
            get
            {
                return this.matrix[row, column];
            }
            set
            {
                this.matrix[row, column] = value;
            }
        }
    }
}
