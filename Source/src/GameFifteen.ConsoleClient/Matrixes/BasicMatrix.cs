namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    public abstract class BasicMatrix:IPrintable,IEnumerable
    {
        protected readonly int[] DirectionRow = { -1, 0, 1, 0 };
        protected readonly int[] DirectionColumn = { 0, 1, 0, -1 };
        
        protected int emptyCellRow;
        protected int emptyCellColumn;

        protected string[,] matrix;
        public abstract void InitializeMatrix();

        public bool IsNextCellValid(int direction)
        {
            var directions = GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;

            return isRowValid && isColumnValid;
        }

        public int[] GetDirections(int direction)
        {
            int nextCellRow = this.emptyCellRow + DirectionRow[direction];
            int nextCellColumn = this.emptyCellColumn + DirectionColumn[direction];
            return new int[] { nextCellRow, nextCellColumn };
        }

        public abstract string this[int row, int column] { get; set; }
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
    }
}
