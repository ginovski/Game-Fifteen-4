// <copyright file="IPrinter.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// IPrinter interface
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient.Interfaces
{
    using System;
    using System.Linq;

    /// <summary>
    /// The IPrinter strategy Interface
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        /// Prints a string message
        /// </summary>
        /// <param name="message">The message</param>
        void Print(string message);

        /// <summary>
        /// Prints an IPrintable 
        /// </summary>
        /// <param name="printable">The IPrintable</param>
        void Print(IPrintable printable);

        /// <summary>
        /// Prints the top scores given an array of scores
        /// </summary>
        /// <param name="scores">An array of scores</param>
        void PrintTopScores(string[] scores);

        /// <summary>
        /// Prints the start screen
        /// </summary>
        void PrintStartScreen();

        /// <summary>
        /// Prints the End screen of the game
        /// </summary>
        /// <param name="victory">Represents if the player is victorious</param>
        /// <param name="score">The score</param>
        void PrintEndScreen(bool victory, int score);
    }
}
