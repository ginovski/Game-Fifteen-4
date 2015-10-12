namespace GameFifteen.ConsoleClient.ScoreControls
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    class ScoreController
    {
        private string[] GetTopScoresFromFile()
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

        private void UpgradeTopScore(int turn)
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

        private void UpgradeTopScoreInFile(IOrderedEnumerable<Player> sortedScores)
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

        private Player[] UpgradeTopScorePairs(string[] topScores)
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
    }
}
