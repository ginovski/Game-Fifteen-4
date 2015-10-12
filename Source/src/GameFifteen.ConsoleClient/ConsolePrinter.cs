using System.Text;

namespace GameFifteen.ConsoleClient
{
    using System;
    using System.Linq;

    class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
        public void Print(IPrintable printable)
        {
            Console.WriteLine(printable.ToPrintable());
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
        public void PrintTopScores(Scoreboard scoreboard)
        {
            var players = scoreboard.GetPlayers();

            if (players.Count == 0)
            {
                this.Print(Constants.NoScoresInFile);
                return;
            }

            this.Print("Scoreboard");

            var scoreBoardAsString = new StringBuilder();

            for (int i = 0; i < players.Count; i++)
            {
                scoreBoardAsString.AppendFormat("{0}. {1} --> {2} moves", i + 1, players[i].Name, players[i].MovesCount);
                scoreBoardAsString.AppendLine();
            }

            scoreBoardAsString.AppendLine();
            this.Print(scoreBoardAsString.ToString());
        }
    }
}
