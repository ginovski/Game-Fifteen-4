namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Collections;

    internal class Matrix : BasicMatrix, IEnumerable
    {
        public Matrix()
            : base()
        {
        }

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
