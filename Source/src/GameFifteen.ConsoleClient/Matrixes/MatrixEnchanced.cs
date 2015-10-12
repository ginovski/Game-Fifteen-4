namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Text;

    using GameFifteen.ConsoleClient.Interfaces;

    internal class MatrixEnchanced : BasicMatrix, IPrintable
    {
        private int turn;
        private Matrix innerMatrix;
        public MatrixEnchanced()
            : base()
        {
            this.innerMatrix = new Matrix();
            this.turn = 0;
        }

        public override void InitializeMatrix()
        {
            this.innerMatrix.InitializeMatrix();
            this.EmptyCells = this.innerMatrix.EmptyCells;
        }
        public void ShuffleMatrix()
        {
            var random = new Random();
            int shuffles = random.Next(Constants.GameBoardSize, Constants.GameBoardSize * 10);
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

            this.turn++;
        }

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
                    matrixBuilder.Append(String.Format("{0,3}", this.innerMatrix[row, column]));
                }

                matrixBuilder.Append(" |\n");
            }
            matrixBuilder.Append(horizontalBorder);
            return matrixBuilder.ToString();


        }
    }
}
