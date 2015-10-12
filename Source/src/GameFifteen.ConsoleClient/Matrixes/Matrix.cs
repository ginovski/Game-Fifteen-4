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

        public override void InitializeMatrix()
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

            var emptyCellRow = Constants.GameBoardRows - 1;
            var emptyCellColumn = Constants.GameBoardColumns - 1;

            this.EmptyCells[0] = emptyCellRow;
            this.EmptyCells[1] = emptyCellColumn;
            this.matrix[emptyCellRow, emptyCellColumn] = Constants.EmptyCellValue;
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
