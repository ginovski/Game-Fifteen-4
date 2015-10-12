// <copyright file="Engine.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// Engine Class
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient.Engines
{
    using System;
    using System.Text.RegularExpressions;

    using GameFifteen.ConsoleClient.Interfaces;
    using GameFifteen.ConsoleClient.Matrixes;
    using GameFifteen.ConsoleClient.ScoreControls;

    /// <summary>
    /// The engine of the game
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The turns made in the game
        /// </summary>
        private int turn = 0;

        /// <summary>
        /// The printer used in the engine
        /// </summary>
        private IPrinter printer;
        
        /// <summary>
        /// The matrix used in the engine
        /// </summary>
        private MatrixEnhanced matrix;

        /// <summary>
        /// The score controller used in the engine
        /// </summary>
        private ScoreController scoreController;

        /// <summary>
        /// Initializes a new instance of the Engine class
        /// </summary>
        public Engine()
        {
            this.printer = new ConsolePrinter();
            this.matrix = new MatrixEnhanced();
            this.scoreController = new ScoreController();
        }

        /// <summary>
        /// Starts the Engine
        /// </summary>
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
                        this.EndGame();
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

        /// <summary>
        /// Marks the End of the game
        /// </summary>
        private void EndGame()
        {
            string moves = this.turn == 1 ? "1 move" : string.Format("{0} moves", this.turn);

            this.printer.PrintEndScreen(true, (int)moves);

            string[] topScores = this.scoreController.GetTopScoresFromFile();
            if (topScores[Constants.TopScoresAmount - 1] != null)
            {
                string lowestScore = Regex.Replace(topScores[Constants.TopScoresAmount - 1], Constants.TopScoresPersonPattern, @"$2");
                if (int.Parse(lowestScore) < this.turn)
                {
                    this.printer.PrintEndScreen(false, Constants.TopScoresAmount);
                    return;
                }
            }

            this.scoreController.UpgradeTopScore(this.turn);
        }

        /// <summary>
        /// Makes the next move in the matrix
        /// </summary>
        /// <param name="cellNumber">A number in the matrix</param>
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

        /// <summary>
        /// Gets the cell number with a given direction
        /// </summary>
        /// <param name="cellNumber">The given direction</param>
        /// <returns>The cell number</returns>
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
