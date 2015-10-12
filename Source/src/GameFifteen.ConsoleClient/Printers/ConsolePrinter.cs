namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    using GameFifteen.ConsoleClient.Interfaces;


    class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
        public void Print(IPrintable printable)
        {
            Console.Clear();
            PrintStartScreen();
            Print(printable.ToPrintable());
        }

        public void PrintStartScreen()
        {
            Print("Welcome to the game \"15\". Please try to arrange the numbers sequentially. ");
            Print("Use 'top' to view the top scoreboard, " +
                              "'restart' to start a new game and 'exit'  to quit the game.");
        }

        public void PrintEndScreen()
        {
            throw new NotImplementedException();
        }
        public void PrintTopScores(string[] scores)
        {
            throw new NotImplementedException();
        }
    }
}
