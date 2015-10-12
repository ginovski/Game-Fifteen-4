// <copyright file="Constants.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// Static class Constants
// </summary>
// <author>GameFifteen4Team</author>

namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    /// <summary>
    /// Internal static class that holds all of the constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Constant for EmptyCellValue.
        /// </summary>
        internal const string EmptyCellValue = " ";

        /// <summary>
        /// Constant for GameBoardRows.
        /// </summary>
        internal const int GameBoardRows = 4;

        /// <summary>
        /// Constant for GameBoardColumns.
        /// </summary>
        internal const int GameBoardColumns = 4;

        /// <summary>
        /// Constant for GameBoardSize.
        /// </summary>
        internal const int GameBoardSize = 16;

        /// <summary>
        /// Constant for TopScoresAmount.
        /// </summary>
        internal const int TopScoresAmount = 5;

        /// <summary>
        /// Constant for TopScoresFileName.
        /// </summary>
        internal const string TopScoresFileName = "Top.txt";

        /// <summary>
        /// Constant for TopScoresPersonPattern.
        /// </summary>
        internal const string TopScoresPersonPattern = @"^\d+\. (.+) --> (\d+) moves?$";

        /// <summary>
        /// Constant for InvalidPlayer.
        /// </summary>
        internal const string InvalidPlayer = "Player cannot be null.";

        /// <summary>
        /// Constant for InvalidPlayerName.
        /// </summary>
        internal const string InvalidPlayerName = "Name must be a non - empty string.";

        /// <summary>
        /// Constant for NegativeMove.
        /// </summary>
        internal const string NegativeMove = "Moves cannot be negative.";

        /// <summary>
        /// Constant for PlayerName.
        /// </summary>
        internal const string PlayerName = "Please enter your name for the top scoreboard: ";

        /// <summary>
        /// Constant for NonEmptyPlayerName.
        /// </summary>
        internal const string NonEmptyPlayerName = "Please enter a non empty name: ";

        /// <summary>
        /// Constant for EnterNumberToMove.
        /// </summary>
        internal const string EnterNumberToMove = "Enter a number to move: ";

        /// <summary>
        /// Constant for Goodbye.
        /// </summary>
        internal const string Goodbye = "Goodbye!";

        /// <summary>
        /// Constant for CellDoesNotExist.
        /// </summary>
        internal const string CellDoesNotExist = "No such cell!";

        /// <summary>
        /// Constant for IllegalMove.
        /// </summary>
        internal const string IllegalMove = "Illegal move";

        /// <summary>
        /// Constant for InvalidNumber.
        /// </summary>
        internal const string InvalidNumber = "Invalid number";

        /// <summary>
        /// Constant for IllegalCommand.
        /// </summary>
        internal const string IllegalCommand = "Illegal command";
    }
}
