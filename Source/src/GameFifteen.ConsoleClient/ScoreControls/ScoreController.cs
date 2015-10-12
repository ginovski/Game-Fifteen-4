// <copyright file="ScoreController.cs" company="GameFifteen4Team">
// Copyright(c) 2015 Team "Game-Fifteen-4"
// </copyright>
// <summary>
// ScoreController Class
// </summary>
// <author>GameFifteen4Team</author>
namespace GameFifteen.ConsoleClient.ScoreControls
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A controller for the scores in the game
    /// </summary>
    public class ScoreController
    {
        /// <summary>
        /// Gets all the top scores from a file
        /// </summary>
        /// <returns>An array of strings which contains the top scores</returns>
        public string[] GetTopScoresFromFile()
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

        /// <summary>
        /// Updates the top score
        /// </summary>
        /// <param name="turn">The number of turns a player has made</param>
        public void UpgradeTopScore(int turn)
        {
            string[] topScores = this.GetTopScoresFromFile();
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            if (name == string.Empty)
            {
                name = "Anonymous";
            }

            topScores[Constants.TopScoresAmount] = string.Format("0. {0} --> {1} move", name, turn);
            Array.Sort(topScores);

            var topScoresPairs = this.UpgradeTopScorePairs(topScores);
            var sortedScores = topScoresPairs.OrderBy(x => x.MovesCount).ThenBy(x => x.Name);

            this.UpgradeTopScoreInFile(sortedScores);
        }

        /// <summary>
        /// Upgrades the score in a file
        /// </summary>
        /// <param name="sortedScores">An array of players</param>
        public void UpgradeTopScoreInFile(IOrderedEnumerable<Player> sortedScores)
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

        /// <summary>
        /// Upgrades the top score pairs
        /// </summary>
        /// <param name="topScores">An array of top scores</param>
        /// <returns>An array of players</returns>
        public Player[] UpgradeTopScorePairs(string[] topScores)
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
