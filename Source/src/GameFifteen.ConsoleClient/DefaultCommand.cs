using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.ConsoleClient
{
    /// <summary>Represents a default command.</summary>
    internal class DefaultCommand : Command
    {
        private readonly IPrinter printer;
        private readonly int[,] matrix;
        private readonly Point emptyPoint;
        private readonly string inputString;
        private bool isPlayerMoved = false;


        /// <summary>Constructor.</summary>
        /// <param name="matrix" type="int[,]">The matrix.</param>
        /// <param name="printer" type="IRenderer">The renderer.</param>
        /// <param name="emptyPoint" type="Point">The empty point.</param>
        /// <param name="inputString" type="string">The input string.</param>
        public DefaultCommand(int[,] matrix, IPrinter printer, Point emptyPoint, string inputString)
        {
            this.matrix = matrix;
            this.printer = printer;
            this.emptyPoint = emptyPoint;
            this.inputString = inputString;
        }

        /// <summary>Gets or sets a value indicating whether this object is player moved.</summary>
        /// <value>true if this object is player moved, false if not.</value>
        public bool IsPlayerMoved
        {
            get { return this.isPlayerMoved; }
            private set { this.isPlayerMoved = value; }
        }

        /// <summary>Executes this object.</summary>
        public override void Execute()
        {
            this.IsPlayerMoved = false;
            int number = 0;
            if (ValidMooveCommand(ref number, this.inputString))
            {
                ExecuteMooveCommand(ref number, this.matrix, this.emptyPoint);
            }
        }

        private void ExecuteMooveCommand(ref int number, int[,] currentMatrix, Point emptyPoint)
        {
            Point[] directions = Dirs.GetDirection;
            int directionsCount = directions.GetLength(0);
            int matrixLength = currentMatrix.GetLength(0);

            Point newPoint = new Point(0, 0);
            for (int i = 0; i <= directionsCount; i++)
            {
                if (i == matrix.GetLength(0))
                {
                    printer.Print(Constants.IllegalMove);
                    break;
                }
                newPoint.Row = emptyPoint.Row + directions[i].Row;
                newPoint.Col = emptyPoint.Col + directions[i].Col;
               /* if (OutOfMatrixChecker.CheckIfOutOfMatrix(newPoint, matrixLength))
                {
                    continue;
                }
                if (currentMatrix[newPoint.Row, newPoint.Col] == number)
                {
                    EmptyCellMover.MoveEmptyCell(emptyPoint, new Point(newPoint.Row, newPoint.Col), currentMatrix);
                    this.IsPlayerMoved = true;
                    break;
                }
                */
            }
        }

        private bool ValidMooveCommand(ref int number, string stringInput)
        {
            bool isNumber = int.TryParse(stringInput, out number);
            int lastNumber = Constants.GameBoardSize * Constants.GameBoardSize;

            if (!isNumber)
            {
                printer.Print(Constants.IllegalCommand);
                return false;
            }

            if (number >= lastNumber || number <= 0)
            {
                printer.Print(Constants.InvalidNumber);
                return false;
            }

            return true;
        }
    }
}
