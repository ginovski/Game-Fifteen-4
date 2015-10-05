namespace Game15
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Program
    {
        private const string EmptyCellValue = " ";

        private const int GameBoardRows = 4;

        private const int GameBoardColumns = 4;

        private const int GameBoardSize = 16;

        private const int TopScoresAmount = 5;

        private const string TopScoresFileName = "Top.txt";

        private const string TopScoresPersonPattern = @"^\d+\. (.+) --> (\d+) moves?$";

        private static readonly int[] DirectionRow = { -1, 0, 1, 0 };

        private static readonly int[] DirectionColumn = { 0, 1, 0, -1 };

        private static readonly Random Random = new Random();

        private static int emptyCellRow;

        private static int emptyCellColumn;

        private static string[,] matrix;

        private static int turn;

        private static int CellNumberToDirection(int cellNumber)
        {
            int direction = -1;
            for (int dir = 0; dir < DirectionRow.Length; dir++)
            {
                bool isDirValid = IsNextCellValid(dir);

                if (isDirValid)
                {
                    int nextCellRow = emptyCellRow + DirectionRow[dir];
                    int nextCellColumn = emptyCellColumn + DirectionColumn[dir];

                    if (matrix[nextCellRow, nextCellColumn] == cellNumber.ToString())
                    {
                        direction = dir;
                        break;
                    }
                }
            }

            return direction;
        }

        private static void TheEnd()
        {
            string moves = turn == 1 ? "1 move" : string.Format("{0} moves", turn);

            Console.WriteLine("Congratulations! You won the game in {0}.", moves);

            string[] topScores = GetTopScoresFromFile();
            if (topScores[TopScoresAmount - 1] != null)
            {
                string lowestScore = Regex.Replace(topScores[TopScoresAmount - 1], TopScoresPersonPattern, @"$2");
                if (int.Parse(lowestScore) < turn)
                {
                    Console.WriteLine("You couldn't get in the top {0} scoreboard.", TopScoresAmount);
                    return;
                }
            }

            UpgradeTopScore();
        }

        private static string[] GetTopScoresFromFile()
        {
            try
            {
                var topScores = new string[TopScoresAmount + 1];
                var topReader = new StreamReader(TopScoresFileName);
                using (topReader)
                {
                    int line = 0;
                    while (!topReader.EndOfStream && line < TopScoresAmount)
                    {
                        topScores[line] = topReader.ReadLine();
                        line++;
                    }
                }

                return topScores;
            }
            catch (FileNotFoundException)
            {
                var topWriter = new StreamWriter(TopScoresFileName);
                using (topWriter)
                {
                    topWriter.Write(string.Empty);
                }

                return new string[TopScoresAmount];
            }
        }

        private static void InitializeMatrix()
        {
            matrix = new string[GameBoardRows, GameBoardColumns];

            int cellValue = 1;
            for (int row = 0; row < GameBoardRows; row++)
            {
                for (int column = 0; column < GameBoardColumns; column++)
                {
                    matrix[row, column] = cellValue.ToString();

                    cellValue++;
                }
            }

            emptyCellRow = GameBoardRows - 1;
            emptyCellColumn = GameBoardColumns - 1;

            matrix[emptyCellRow, emptyCellColumn] = EmptyCellValue;
        }

        private static bool IsNextCellValid(int direction)
        {
            // TODO: Can be extracted into method for getting next row/col
            int nextCellRow = emptyCellRow + DirectionRow[direction];
            int nextCellColumn = emptyCellColumn + DirectionColumn[direction];

            bool isRowValid = nextCellRow >= 0 && nextCellRow < GameBoardRows;
            bool isColumnValid = nextCellColumn >= 0 && nextCellColumn < GameBoardColumns;
            bool isCellValid = isRowValid && isColumnValid;

            return isCellValid;
        }

        private static bool CheckIfNumbersAreSequential()
        {
            // TODO: Can be extracted to method
            bool isEmptyCellInPlace = emptyCellRow == GameBoardRows - 1 && emptyCellColumn == GameBoardColumns - 1;
            if (!isEmptyCellInPlace)
            {
                return false;
            }

            int cellValue = 1;

            for (int row = 0; row < GameBoardRows; row++)
            {
                for (int column = 0; column < GameBoardColumns && cellValue < GameBoardSize; column++)
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
            matrix[nextCellRow, nextCellColumn] = EmptyCellValue;

            emptyCellRow = nextCellRow;
            emptyCellColumn = nextCellColumn;

            turn++;
        }

        private static void NextMove(int cellNumber)
        {
            if (cellNumber <= 0 || cellNumber >= GameBoardSize)
            {
                PrintCellDoesNotExistMessage();
                return;
            }

            int direction = CellNumberToDirection(cellNumber);
            if (direction == -1)
            {
                PrintIllegalMoveMessage();
                return;
            }

            MoveCell(direction);
            PrintMatrix();
        }

        public static void PlayGame()
        {
            while (true)
            {
                InitializeMatrix();
                ShuffleMatrix();

                turn = 0;
                PrintWelcomeMessage();
                PrintMatrix();

                while (true)
                {
                    PrintNextMoveMessage();

                    string consoleInputLine = Console.ReadLine();
                    int cellNumber;
                    if (int.TryParse(consoleInputLine, out cellNumber))
                    {
                        NextMove(cellNumber);
                        if (CheckIfNumbersAreSequential())
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
                                PrintGoodbye();
                                return;

                            default:
                                PrintIllegalCommandMessage();
                                break;
                        }
                    }
                }
            }
        }

        private static void PrintCellDoesNotExistMessage()
        {
            Console.WriteLine("That cell does not exist in the matrix.");
        }

        private static void PrintGoodbye()
        {
            Console.WriteLine("Good bye!");
        }

        private static void PrintIllegalCommandMessage()
        {
            Console.WriteLine("Illegal command!");
        }

        private static void PrintIllegalMoveMessage()
        {
            Console.WriteLine("Illegal move!");
        }

        private static void PrintNextMoveMessage()
        {
            Console.Write("Enter a number to move: ");
        }

        private static void PrintMatrix()
        {
            // TODO: Horizontal border is the same, extract it to different method and call it once
            var horizontalBorder = new StringBuilder("  ");
            for (int i = 0; i < GameBoardColumns; i++)
            {
                horizontalBorder.Append("---");
            }

            horizontalBorder.Append("- ");
            Console.WriteLine(horizontalBorder);

            for (int row = 0; row < GameBoardRows; row++)
            {
                Console.Write(" |");
                for (int column = 0; column < GameBoardColumns; column++)
                {
                    Console.Write("{0,3}", matrix[row, column]);
                }

                Console.WriteLine(" |");
            }

            Console.WriteLine(horizontalBorder);
        }

        private static void PrintTopScores()
        {
            Console.WriteLine("Scoreboard:");
            string[] topScores = GetTopScoresFromFile();
            if (topScores[0] == null)
            {
                Console.WriteLine("There are no scores to display yet.");
            }
            else
            {
                foreach (string score in topScores)
                {
                    if (score != null)
                    {
                        Console.WriteLine(score);
                    }
                }
            }
        }

        private static void PrintWelcomeMessage()
        {
            Console.Write("Welcome to the game \"15\". ");
            Console.WriteLine("Please try to arrange the numbers sequentially. ");
            Console.WriteLine("Use 'top' to view the top scoreboard, " +
                              "'restart' to start a new game and 'exit'  to quit the game.");
        }

        private static void ShuffleMatrix()
        {            
            int shuffles = Random.Next(GameBoardSize, GameBoardSize * 100);
            for (int i = 0; i < shuffles; i++)
            {
                int direction = Random.Next(DirectionRow.Length);
                if (IsNextCellValid(direction))
                {
                    MoveCell(direction);
                }
            }

            if (CheckIfNumbersAreSequential())
            {
                ShuffleMatrix();
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

            topScores[TopScoresAmount] = string.Format("0. {0} --> {1} move", name, turn);
            Array.Sort(topScores);

            var topScoresPairs = UpgradeTopScorePairs(topScores);
            var sortedScores = topScoresPairs.OrderBy(x => x.MovesCount).ThenBy(x => x.Name);

            UpgradeTopScoreInFile(sortedScores);
        }

        private static void UpgradeTopScoreInFile(IOrderedEnumerable<Player> sortedScores)
        {
            var topWriter = new StreamWriter(TopScoresFileName);
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

            int arraySize = Math.Min(TopScoresAmount - startIndex + 1, TopScoresAmount);
            var topScoresPairs = new Player[arraySize];
            for (int topScoresPairsIndex = 0; topScoresPairsIndex < arraySize; topScoresPairsIndex++)
            {
                int topScoresIndex = topScoresPairsIndex + startIndex;

                string name = Regex.Replace(topScores[topScoresIndex], TopScoresPersonPattern, @"$1");
                string score = Regex.Replace(topScores[topScoresIndex], TopScoresPersonPattern, @"$2");

                int scoreInt = int.Parse(score);
                topScoresPairs[topScoresPairsIndex] = new Player(name, scoreInt);
            }

            return topScoresPairs;
        }

        private static void Main()
        {
            PlayGame();
        }
    }
}
