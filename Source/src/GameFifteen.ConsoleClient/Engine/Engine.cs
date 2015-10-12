namespace GameFifteen.ConsoleClient.Engines
{
    using System;
    using System.Text.RegularExpressions;

    using GameFifteen.ConsoleClient.Interfaces;
    using GameFifteen.ConsoleClient.Matrixes;
    using GameFifteen.ConsoleClient.ScoreControls;

    public class Engine
    {
        private int turn = 0;
        private IPrinter printer;
        private MatrixEnchanced matrix;
        private ScoreController scoreController;

        public Engine()
        {
            this.printer = new ConsolePrinter();
            this.matrix = new MatrixEnchanced();
            this.scoreController = new ScoreController();
        }

        public void Start()
        {
            this.matrix.InitializeMatrix();
            this.matrix.ShuffleMatrix();
            this.printer.PrintStartScreen();
            this.printer.Print(this.matrix);

            while (true)
            {
                this.printer.Print(Constants.EnterNumberToMove);

                string consoleInputLine = Console.ReadLine();
                int cellNumber;
                if (int.TryParse(consoleInputLine, out cellNumber))
                {
                    this.NextMove(cellNumber);
                    if (this.matrix.AreNumbersSequential())
                    {
                        this.TheEnd();
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
                            this.printer.PrintTopScores(this.scoreController.GetTopScoresFromFile());
                            break;

                        case "exit":
                            this.printer.Print(Constants.Goodbye);
                            return;

                        default:
                            this.printer.Print(Constants.IllegalCommand);
                            break;
                    }
                }
            }
        }

        private void TheEnd()
        {
            string moves = this.turn == 1 ? "1 move" : string.Format("{0} moves", this.turn);

            this.printer.Print(string.Format("Congratulations! You won the game in {0}.", moves));

            string[] topScores = this.scoreController.GetTopScoresFromFile();
            if (topScores[Constants.TopScoresAmount - 1] != null)
            {
                string lowestScore = Regex.Replace(topScores[Constants.TopScoresAmount - 1], Constants.TopScoresPersonPattern, @"$2");
                if (int.Parse(lowestScore) < this.turn)
                {
                    this.printer.Print(string.Format("You couldn't get in the top {0} scoreboard.", Constants.TopScoresAmount));
                    return;
                }
            }

            this.scoreController.UpgradeTopScore(this.turn);
        }

        private void NextMove(int cellNumber)
        {
            if (cellNumber <= 0 || cellNumber >= Constants.GameBoardSize)
            {
                this.printer.Print(Constants.CellDoesNotExist);
                return;
            }

            int direction = this.CellNumberToDirection(cellNumber);
            if (direction == -1)
            {
                this.printer.Print(Constants.IllegalMove);
                return;
            }

            this.matrix.MoveCell(direction);
            this.turn++;
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
