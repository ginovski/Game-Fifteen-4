namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Collections;
    using System.Linq;

    public abstract class BasicMatrix : IEnumerable
    {
        private int[] directionRow = { -1, 0, 1, 0 };
        private int[] directionColumn = { 0, 1, 0, -1 };
        private string[,] matrix;

        public BasicMatrix()
        {
            this.EmptyCells = new int[2];
        }

        public int[] EmptyCells { get; set; }

        protected int[] DirectionRow
        {
            get
            {
                return this.directionRow;
            }

            set
            {
                this.directionRow = value;
            }
        }

        protected int[] DirectionColumn
        {
            get
            {
                return this.directionColumn;
            }

            set
            {
                this.directionColumn = value;
            }
        }

        protected string[,] Matrix
        {
            get
            {
                return this.matrix;
            }

            set
            {
                this.matrix = value;
            }
        }

        public abstract string this[int row, int column] { get; set; }

        public bool IsNextCellValid(int direction)
        {
            var directions = this.GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;

            return isRowValid && isColumnValid;
        }

        public int[] GetDirections(int direction)
        {
            var emptyCellRow = this.EmptyCells[0];
            var emptyCellColumn = this.EmptyCells[1];
            int nextCellRow = emptyCellRow + this.directionRow[direction];
            int nextCellColumn = emptyCellColumn + this.directionColumn[direction];
            return new int[] { nextCellRow, nextCellColumn };
        }

        public abstract void InitializeMatrix();

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
