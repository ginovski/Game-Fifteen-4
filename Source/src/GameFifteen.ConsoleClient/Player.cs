namespace GameFifteen.ConsoleClient
{
    using System;

    /// <summary>
    /// Class containing the info of the Player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Store for the name property.
        /// </summary>
        private string name;

        /// <summary>
        /// Store for the score property.
        /// </summary>       
        private int movesCount;

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="movesCount">Player's number of moves</param>
        public Player(string name, int movesCount)
        {
            this.Name = name;
            this.MovesCount = movesCount;
        }

        /// <summary>
        /// Gets or sets Player name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(Constants.InvalidPlayerName);
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets Player moves count
        /// </summary>
        public int MovesCount
        {
            get
            {
                return this.movesCount;
            }

            set
            {
                this.movesCount = value;
            }
        }
    }
}