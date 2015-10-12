namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Collections;
    using System.Text;

    internal class Matrix : BasicMatrix, IPrintable, IEnumerable
    {
        public Matrix()
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

            this.emptyCellRow = Constants.GameBoardRows - 1;
            this.emptyCellColumn = Constants.GameBoardColumns - 1;

            this.matrix[this.emptyCellRow, this.emptyCellColumn] = Constants.EmptyCellValue;
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
