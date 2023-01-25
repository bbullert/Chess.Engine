using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Extensions
{
    public static partial class PieceExtensions
    {
        public static char ToAlgebraicNotation(this IPiece piece)
        {
            if (piece.Type == PieceType.Pawn)
                return default(char);
            else if (piece.Type == PieceType.Knight)
                return 'N';
            else
                return piece.Type.ToString().Substring(0, 1).ToUpper()[0];
        }
    }
}
