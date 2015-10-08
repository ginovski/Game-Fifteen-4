namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Constants
    {
        internal const string EmptyCellValue = " ";
        internal const int GameBoardRows = 4;
        internal const int GameBoardColumns = 4;
        internal const int GameBoardSize = 16;
        internal const int TopScoresAmount = 5;
        internal const string TopScoresFileName = "Top.txt";
        internal const string TopScoresPersonPattern = @"^\d+\. (.+) --> (\d+) moves?$";

        internal const string InvalidPlayer = "Player cannot be null.";
        internal const string InvalidPlayerName = "Name must be a non - empty string.";
        internal const string NegativeMove = "Moves cannot be negative.";
        internal const string PlayerName = "Please enter your name for the top scoreboard: ";
        internal const string NonEmptyPlayerName = "Please enter a non empty name: ";
        internal const string EnterNumberToMove = "Enter a number to move: ";
        internal const string Goodbye = "Goodbye!";
        internal const string CellDoesNotExist = "No such cell!";
        internal const string IllegalMove = "Illegal move";
        internal const string InvalidNumber = "Invalid number";
        internal const string IllegalCommand = "Illegal command";
    }
}
