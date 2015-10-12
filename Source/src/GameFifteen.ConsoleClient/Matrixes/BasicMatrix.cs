namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    using GameFifteen.ConsoleClient.Interfaces;

    public abstract class BasicMatrix : IEnumerable
    {
        protected readonly int[] DirectionRow = { -1, 0, 1, 0 };
        protected readonly int[] DirectionColumn = { 0, 1, 0, -1 };
        protected string[,] matrix;
        public BasicMatrix()
        {
            this.EmptyCells = new int[2];
        }


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
            var emptyCellRow=this.EmptyCells[0];
            var emptyCellColumn = this.EmptyCells[1];
            int nextCellRow = emptyCellRow + DirectionRow[direction];
            int nextCellColumn = emptyCellColumn + DirectionColumn[direction];
            return new int[] { nextCellRow, nextCellColumn };
        }
        public int[] EmptyCells{ get; set; }
        public abstract string this[int row, int column] { get; set; }
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
