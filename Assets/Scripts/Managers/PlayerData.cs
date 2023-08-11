namespace GT
{
    public struct PlayerData
    {
        public int destroyedBombCount;
        public int remainingSeconds;

        public string lastUser; //No va a haber a menos que haga un score que ingrese al puntaje
        public int lastBombScore;
        public int lastTimeScore;
        public int lastTotalScore;
    }
}