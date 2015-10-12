// <copyright file="ConsolePrinter.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// ConsolerPrinter Class
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    using GameFifteen.ConsoleClient.Interfaces;

    /// <summary>
    /// The ConsolePrinter class
    /// </summary>
    public class ConsolePrinter : IPrinter
    {
        /// <summary>
        /// Prints a message
        /// </summary>
        /// <param name="message">The message</param>
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prints an IPrintable
        /// </summary>
        /// <param name="printable">The IPrintable</param>
        public void Print(IPrintable printable)
        {
            Console.Clear();
            this.PrintStartScreen();
            this.Print(printable.ToPrintable());
        }

        /// <summary>
        /// Prints the Start Screen
        /// </summary>
        public void PrintStartScreen()
        {
            this.Print("Welcome to the game \"15\". Please try to arrange the numbers sequentially. ");
            this.Print("Use 'top' to view the top scoreboard, " +
                              "'restart' to start a new game and 'exit'  to quit the game.");
        }

        /// <summary>
        /// Prints the EndScreen
        /// </summary>
        /// <param name="victory">A variable used to determine if you are victorious</param>
        /// <param name="score">The score</param>
        public void PrintEndScreen(bool victory, int score)
        {
            if (victory)
            {
                this.Print(string.Format("Congratulations! You won the game in {0}.", score));
            }
            else
            {
                this.Print(string.Format("You couldn't get in the top {0} scoreboard.", score));
            }
        }

        /// <summary>
        /// Prints the top score
        /// </summary>
        /// <param name="topScores">Requires an array of strings</param>
        public void PrintTopScores(string[] topScores)
        {
            this.Print("Scoreboard:");
            if (topScores[0] == null)
            {
                this.Print("There are no scores to display yet.");
            }
            else
            {
                foreach (string score in topScores)
                {
                    if (score != null)
                    {
                        this.Print(score);
                    }
                }
            }
        }
    }
}
