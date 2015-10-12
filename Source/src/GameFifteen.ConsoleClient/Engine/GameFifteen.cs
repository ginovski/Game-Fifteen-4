namespace GameFifteen.ConsoleClient.Engines
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Program
    {
        private static readonly int[] DirectionRow = { -1, 0, 1, 0 };

        private static readonly int[] DirectionColumn = { 0, 1, 0, -1 };

        private static readonly Random Random = new Random();

        private static int emptyCellRow;

        private static int emptyCellColumn;

        private static string[,] matrix;

        private static int turn;

        public static void PlayGame()
        {
            InitializeMatrix();
            ShuffleMatrix();

            turn = 0;
            PrintWelcomeMessage();
            PrintMatrix();

            while (true)
            {
                PrintMessage(Constants.EnterNumberToMove);

                string consoleInputLine = Console.ReadLine();
                int cellNumber;
                if (int.TryParse(consoleInputLine, out cellNumber))
                {
                    NextMove(cellNumber);
                    if (AreNumbersSequential())
                    {
                        TheEnd();
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
                            PrintTopScores();
                            break;

                        case "exit":
                            PrintMessage(Constants.Goodbye);
                            return;

                        default:
                            PrintMessage(Constants.IllegalCommand);
                            break;
                    }
                }
            }
        }

        private static int CellNumberToDirection(int cellNumber)
        {
            for (int direction = 0; direction < DirectionRow.Length; direction++)
            {
                bool isDirValid = IsNextCellValid(direction);

                if (isDirValid)
                {
                    int nextCellRow = emptyCellRow + DirectionRow[direction];
                    int nextCellColumn = emptyCellColumn + DirectionColumn[direction];

                    if (matrix[nextCellRow, nextCellColumn] == cellNumber.ToString())
                    {
                        return direction;
                    }
                }
            }
            return -1;
        }

        private static void NextMove(int cellNumber)
        {
            if (cellNumber <= 0 || cellNumber >= Constants.GameBoardSize)
            {
                PrintMessage(Constants.CellDoesNotExist);
                return;
            }

            int direction = CellNumberToDirection(cellNumber);
            if (direction == -1)
            {
                PrintMessage(Constants.IllegalMove);
                return;
            }

            MoveCell(direction);
            PrintMatrix();
        }

        private static void InitializeMatrix()
        {
            matrix = new string[Constants.GameBoardRows, Constants.GameBoardColumns];

            int cellValue = 1;
            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    matrix[row, column] = cellValue.ToString();

                    cellValue++;
                }
            }

            emptyCellRow = Constants.GameBoardRows - 1;
            emptyCellColumn = Constants.GameBoardColumns - 1;

            matrix[emptyCellRow, emptyCellColumn] = Constants.EmptyCellValue;
        }

        private static bool IsNextCellValid(int direction)
        {
            // TODO: Can be extracted into method for getting next row/col
            int nextCellRow = emptyCellRow + DirectionRow[direction];
            int nextCellColumn = emptyCellColumn + DirectionColumn[direction];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < Constants.GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < Constants.GameBoardColumns;
            bool isCellValid = isRowValid && isColumnValid;

            return isCellValid;
        }

        private static bool AreNumbersSequential()
        {
            // TODO: Can be extracted to method
            bool isEmptyCellInPlace = emptyCellRow == Constants.GameBoardRows - 1 && emptyCellColumn == Constants.GameBoardColumns - 1;
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

        private static void MoveCell(int direction)
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

        private static void ShuffleMatrix()
        {
            int shuffles = Random.Next(Constants.GameBoardSize, Constants.GameBoardSize * 100);
            for (int i = 0; i < shuffles; i++)
            {
                int direction = Random.Next(DirectionRow.Length);
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

        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void PrintMatrix()
        {
            // TODO: Horizontal border is the same, extract it to different method and call it once
            var horizontalBorder = new StringBuilder("  ");
            for (int i = 0; i < Constants.GameBoardColumns; i++)
            {
                horizontalBorder.Append("---");
            }

            horizontalBorder.Append("- ");
            PrintMessage(horizontalBorder.ToString());

            for (int row = 0; row < Constants.GameBoardRows; row++)
            {
                Console.Write(" |");
                for (int column = 0; column < Constants.GameBoardColumns; column++)
                {
                    Console.Write("{0,3}", matrix[row, column]);
                }

                Console.WriteLine(" |");
            }

            PrintMessage(horizontalBorder.ToString());
        }

        private static void TheEnd()
        {
            string moves = turn == 1 ? "1 move" : string.Format("{0} moves", turn);

            PrintMessage(string.Format("Congratulations! You won the game in {0}.", moves));

            string[] topScores = GetTopScoresFromFile();
            if (topScores[Constants.TopScoresAmount - 1] != null)
            {
                string lowestScore = Regex.Replace(topScores[Constants.TopScoresAmount - 1], Constants.TopScoresPersonPattern, @"$2");
                if (int.Parse(lowestScore) < turn)
                {
                    PrintMessage(string.Format("You couldn't get in the top {0} scoreboard.", Constants.TopScoresAmount));
                    return;
                }
            }

            UpgradeTopScore();
        }

        private static void PrintTopScores()
        {
            PrintMessage("Scoreboard:");
            string[] topScores = GetTopScoresFromFile();
            if (topScores[0] == null)
            {
                PrintMessage("There are no scores to display yet.");
            }
            else
            {
                foreach (string score in topScores)
                {
                    if (score != null)
                    {
                        PrintMessage(score);
                    }
                }
            }
        }

        private static void PrintWelcomeMessage()
        {
            PrintMessage("Welcome to the game \"15\". Please try to arrange the numbers sequentially. ");
            PrintMessage("Use 'top' to view the top scoreboard, " +
                              "'restart' to start a new game and 'exit'  to quit the game.");
        }

        private static string[] GetTopScoresFromFile()
        {
            try
            {
                var topScores = new string[Constants.TopScoresAmount + 1];
                var topReader = new StreamReader(Constants.TopScoresFileName);
                using (topReader)
                {
                    int line = 0;
                    while (!topReader.EndOfStream && line < Constants.TopScoresAmount)
                    {
                        topScores[line] = topReader.ReadLine();
                        line++;
                    }
                }

                return topScores;
            }
            catch (FileNotFoundException)
            {
                var topWriter = new StreamWriter(Constants.TopScoresFileName);
                using (topWriter)
                {
                    topWriter.Write(string.Empty);
                }

                return new string[Constants.TopScoresAmount];
            }
        }

        private static void UpgradeTopScore()
        {
            string[] topScores = GetTopScoresFromFile();
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            if (name == string.Empty)
            {
                name = "Anonymous";
            }

            topScores[Constants.TopScoresAmount] = string.Format("0. {0} --> {1} move", name, turn);
            Array.Sort(topScores);

            var topScoresPairs = UpgradeTopScorePairs(topScores);
            var sortedScores = topScoresPairs.OrderBy(x => x.MovesCount).ThenBy(x => x.Name);

            UpgradeTopScoreInFile(sortedScores);
        }

        private static void UpgradeTopScoreInFile(IOrderedEnumerable<Player> sortedScores)
        {
            var topWriter = new StreamWriter(Constants.TopScoresFileName);
            using (topWriter)
            {
                int position = 1;
                foreach (Player pair in sortedScores)
                {
                    string name = pair.Name;
                    int movesCount = pair.MovesCount;
                    string toWrite = string.Format("{0}. {1} --> {2} move", position, name, movesCount);
                    if (movesCount > 1)
                    {
                        toWrite += "s";
                    }

                    topWriter.WriteLine(toWrite);
                    position++;
                }
            }
        }

        private static Player[] UpgradeTopScorePairs(string[] topScores)
        {
            int startIndex = 0;
            while (topScores[startIndex] == null)
            {
                startIndex++;
            }

            int arraySize = Math.Min(Constants.TopScoresAmount - startIndex + 1, Constants.TopScoresAmount);
            var topScoresPairs = new Player[arraySize];
            for (int topScoresPairsIndex = 0; topScoresPairsIndex < arraySize; topScoresPairsIndex++)
            {
                int topScoresIndex = topScoresPairsIndex + startIndex;

                string name = Regex.Replace(topScores[topScoresIndex], Constants.TopScoresPersonPattern, @"$1");
                string score = Regex.Replace(topScores[topScoresIndex], Constants.TopScoresPersonPattern, @"$2");

                int scoreInt = int.Parse(score);
                topScoresPairs[topScoresPairsIndex] = new Player(name, scoreInt);
            }

            return topScoresPairs;
        }

        private static void Main()
        {
            //PlayGame();
            var Engine = new Engine();
            Engine.Start();
        }
    }
}
