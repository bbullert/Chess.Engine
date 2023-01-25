namespace Chess.Engine.Components
{
    public class GameState
    {
        public string ChessBoardState { get; set; }
        public string? CastlingRights { get; set; }
        public string? EnPassantSquareName { get; set; }
        public int FiftyMoveRuleClock { get; set; }
        public string? CapturedPieces { get; set; }
    }
}
