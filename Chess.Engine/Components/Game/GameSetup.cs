using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class GameSetup
    {
        public string InitialChessBoardState { get; set; }
        public int? TimeLimit { get; set; }
        public int TimeIncrement { get; set; }
        public GameMode GameMode { get; set; }
    }
}
