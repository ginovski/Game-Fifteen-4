namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    using GameFifteen.ConsoleClient.Interfaces;

    public class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public void Print(IPrintable printable)
        {
            Console.Clear();
            this.PrintStartScreen();
            this.Print(printable.ToPrintable());
        }

        public void PrintStartScreen()
        {
            this.Print("Welcome to the game \"15\". Please try to arrange the numbers sequentially. ");
            this.Print("Use 'top' to view the top scoreboard, " +
                              "'restart' to start a new game and 'exit'  to quit the game.");
        }

        public void PrintEndScreen()
        {
            throw new NotImplementedException();
        }

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
