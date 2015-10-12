// <copyright file="BasicMatrix.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// BasicMatrix Class
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient.Matrixes
{
    using System;
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// The base Matrix class
    /// </summary>
    public abstract class BasicMatrix : IEnumerable
    {
        /// <summary>
        /// An array representing the direction rows
        /// </summary>
        private int[] directionRow = { -1, 0, 1, 0 };

        /// <summary>
        /// An array representing the direction columns
        /// </summary>
        private int[] directionColumn = { 0, 1, 0, -1 };

        /// <summary>
        /// The matrix of strings used in the program
        /// </summary>
        private string[,] matrix;

        /// <summary>
        /// Initializes a new instance of the BasicMatrix class
        /// </summary>
        public BasicMatrix()
        {
            this.EmptyCells = new int[2];
        }

        /// <summary>
        /// Gets or sets the array representing the empty cells
        /// </summary>
        public int[] EmptyCells { get; set; }

        /// <summary>
        /// Gets or sets the property representing the direction rows
        /// </summary>
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

        /// <summary>
        /// Gets or sets the property representing the direction columns
        /// </summary>
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

        /// <summary>
        /// Gets or sets the property representing the matrix
        /// </summary>
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

        /// <summary>
        /// Represents an indexer
        /// </summary>
        /// <param name="row">Represents the rows</param>
        /// <param name="column">Represents the columns</param>
        /// <returns>The element in the matrix</returns>
        public abstract string this[int row, int column] { get; set; }

        /// <summary>
        /// Returns a boolean value representing if a cell is valid
        /// </summary>
        /// <param name="direction">Represents the direction</param>
        /// <returns>Boolean value</returns>
        public bool IsNextCellValid(int direction)
        {
            var directions = this.GetDirections(direction);
            int nextCellRow = directions[0];
            int nextCellColumn = directions[1];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;

            return isRowValid && isColumnValid;
        }

        /// <summary>
        /// Gets the direction array from a given direction
        /// </summary>
        /// <param name="direction">the direction</param>
        /// <returns>Returns the direction array</returns>
        public int[] GetDirections(int direction)
        {
            var emptyCellRow = this.EmptyCells[0];
            var emptyCellColumn = this.EmptyCells[1];
            int nextCellRow = emptyCellRow + this.directionRow[direction];
            int nextCellColumn = emptyCellColumn + this.directionColumn[direction];
            return new int[] { nextCellRow, nextCellColumn };
        }

        /// <summary>
        /// Abstract class used to Initialize the matrix
        /// </summary>
        public abstract void InitializeMatrix();

        /// <summary>
        /// Iterator Pattern
        /// </summary>
        /// <returns>Returns the values of the matrix</returns>
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
