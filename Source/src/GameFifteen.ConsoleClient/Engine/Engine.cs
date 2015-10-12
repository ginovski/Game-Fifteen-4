namespace GameFifteen.ConsoleClient.Engine
{
    using System;

    using GameFifteen.ConsoleClient.Matrixes;
    using GameFifteen.ConsoleClient.Interfaces;

    public class Engine
    {
        private IPrinter printer;
        private MatrixEnchanced matrix;
        public Engine()
        {
            this.printer = new ConsolePrinter();
            this.matrix = new MatrixEnchanced();
        }

        public void Start()
        {
            this.matrix.InitializeMatrix();
            this.matrix.ShuffleMatrix();
            printer.PrintStartScreen();
            printer.Print(this.matrix);

            while (true)
            {
                printer.Print((Constants.EnterNumberToMove));

                string consoleInputLine = Console.ReadLine();
                int cellNumber;
                if (int.TryParse(consoleInputLine, out cellNumber))
                {
                    NextMove(cellNumber);
                    if (this.matrix.AreNumbersSequential())
                    {
                        //TheEnd();
                        break;
                    }
                }
                else
                {
                    if (consoleInputLine == "restart")
                    {
                        break;
                    }

                    switch (consoleInputLine)
                    {
                        case "top":
                            printer.PrintTopScores(new string[]{"",""});
                            break;

                        case "exit":
                            printer.Print(Constants.Goodbye);
                            return;

                        default:
                            printer.Print(Constants.IllegalCommand);
                            break;
                    }
                }
            }
        }
        private void NextMove(int cellNumber)
        {
            if (cellNumber <= 0 || cellNumber >= Constants.GameBoardSize)
            {
                this.printer.Print(Constants.CellDoesNotExist);
                return;
            }

            int direction = CellNumberToDirection(cellNumber);
            if (direction == -1)
            {
                this.printer.Print(Constants.IllegalMove);
                return;
            }

            this.matrix.MoveCell(direction);
            this.printer.Print(this.matrix);
        }

        private int CellNumberToDirection(int cellNumber)
        {
            for (int direction = 0; direction < 4; direction++)
            {
                bool isDirValid = this.matrix.IsNextCellValid(direction);

                if (isDirValid)
                {
                    var directions = this.matrix.GetDirections(direction);
                    int nextCellRow = directions[0];
                    int nextCellColumn = directions[1];

                    if (this.matrix[nextCellRow, nextCellColumn] == cellNumber.ToString())
                    {
                        return direction;
                    }
                }
            }
            return -1;
        }
    }
}
