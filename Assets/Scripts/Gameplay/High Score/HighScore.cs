namespace GT
{
    public struct HighScore
    {
        public string name;
        public int score;
        public static char Separator { get; } = '_';
        
        public HighScore(string newName, int newScore)
        {
            name = newName;
            score = newScore;
        }
        public static HighScore Default()
        {
            HighScore hs;
            hs.name = "AAA";
            hs.score = 0;
            return hs;
        }

        public override string ToString() 
        {
            string s = "";
            s += name + Separator + score.ToString();
            return s;
        }
        public static HighScore ToStruct(string data)
        {
            HighScore hs;

            System.Collections.Generic.List<string> parameters = new System.Collections.Generic.List<string>();

            foreach (string parameter in data.Split(Separator))
            {
                parameters.Add(parameter);
            }

            hs.name = parameters[0];
            hs.score = int.Parse(parameters[1]);

            return hs;
        }
    }
}
