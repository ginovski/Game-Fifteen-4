namespace GameFifteen
{   //Think of a normal name :D
    struct DvoikaImeRezultat
    {
        private string name;
        private int score;
        //the parameterless constructor needs to be called by :this()
        public DvoikaImeRezultat(string name, int score)
            : this()
        {
            this.Name = name;
            this.Score = score;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }
    }
}