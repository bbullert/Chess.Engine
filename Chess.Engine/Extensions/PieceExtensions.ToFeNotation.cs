using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Extensions
{
    public static partial class PieceExtensions
    {
        public static char ToFeNotation(this IPiece piece)
        {
            var c = piece.Type == PieceType.Knight ? 'N' : piece.Type.ToString().Substring(0, 1)[0];
            return piece.Color == Color.White ? char.ToUpper(c) : char.ToLower(c);
        }
    }
}
