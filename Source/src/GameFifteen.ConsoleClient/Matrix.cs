﻿namespace GameFifteen.ConsoleClient
{
    using System;

    public class Matrix
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
        }
        private void InitializeMatrix()
        {
            this.matrix = new string[Constants.GameBoardRows, Constants.GameBoardColumns];

            int cellValue = 1;
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    matrix[row, column] = cellValue.ToString();

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
                int direction = random.Next(DirectionRow.Length);
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
                    if (matrix[row, column] != cellValue.ToString())
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
            // TODO: Can be extracted into method for getting next row/col
            int nextCellRow = this.emptyCellRow + DirectionRow[direction];
            int nextCellColumn = this.emptyCellColumn + DirectionColumn[direction];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;
            bool isCellValid = isRowValid && isColumnValid;

            return isCellValid;
        }

        private void MoveCell(int direction)
        {
            // TODO: Can be extracted into method for getting next row/col like on line:135
            int nextCellRow = emptyCellRow + DirectionRow[direction];
            int nextCellColumn = emptyCellColumn + DirectionColumn[direction];

            matrix[emptyCellRow, emptyCellColumn] = matrix[nextCellRow, nextCellColumn];
            matrix[nextCellRow, nextCellColumn] = Constants.EmptyCellValue;

            emptyCellRow = nextCellRow;
            emptyCellColumn = nextCellColumn;

            turn++;
        }
    }
}
