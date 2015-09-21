namespace GameFifteen
{
    public struct NameScorePair
    {
        private string name;
        private int score;

        public NameScorePair(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }
    }
}