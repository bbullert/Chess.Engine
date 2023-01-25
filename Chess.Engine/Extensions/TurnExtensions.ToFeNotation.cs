using Chess.Engine.Components;

namespace Chess.Engine.Extensions
{
    public static partial class TurnExtensions
    {
        public static string ToFeNotation(this Turn turn)
        {
            return
                $"{ToShortFeNotation(turn)} " +
                $"{turn.GameState.FiftyMoveRuleClock} " +
                $"{turn.TurnClock}";
        }

        public static string ToShortFeNotation(this Turn turn)
        {
            var castling = turn.GameState.CastlingRights is not null ?
                turn.GameState.CastlingRights.ToString() : "-";

            var enPassant = !string.IsNullOrEmpty(turn.GameState.EnPassantSquareName) ?
                turn.GameState.EnPassantSquareName : "-";

            return
                $"{turn.GameState.ChessBoardState} " +
                $"{turn.Color.ToFeNotation()} " +
                $"{castling} {enPassant}";
        }
    }
}
