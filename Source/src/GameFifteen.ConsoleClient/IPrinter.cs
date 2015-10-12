namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    public interface IPrinter
    {
        void Print(string message);
        void Print(IPrintable printable);
        void PrintTopScores(Scoreboard scoreboard);
        void PrintStartScreen();
        void PrintEndScreen();
    }
}
