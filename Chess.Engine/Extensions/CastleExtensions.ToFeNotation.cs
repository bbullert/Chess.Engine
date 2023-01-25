using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Extensions
{
    public static partial class CastleExtensions
    {
        public static string? ToFeNotation(this IEnumerable<Castle> castlingRights)
        {
            if (!castlingRights.Any())
                return null;

            var castling = string.Empty;
            foreach (var castle in castlingRights)
            {
                castling += castle.Rook.Color == Color.White ?
                    castle.CastleSide.ToString().ToUpper()[0] :
                    castle.CastleSide.ToString().ToLower()[0];
            }

            return castling;
        }
    }
}
