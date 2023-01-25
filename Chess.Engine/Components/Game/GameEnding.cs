namespace Chess.Engine.Components
{
    public class GameEnding
    {
        public string StrongName { get; set; }
        public string Details { get; set; }
        public Player Winner { get; set; }
        public Player Loser { get; set; }
    }
}
