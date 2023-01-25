using Chess.Engine.Components;
using Chess.Engine.Enums;
using Chess.Engine.Exceptions;
using Chess.Engine.Factories;

namespace Chess.Engine.Extensions
{
    public static partial class CharExtensions
    {
        public static IPiece ToPiece(this char value)
        {
            var type = char.ToLower(value) switch
            {
                'p' => PieceType.Pawn,
                'n' => PieceType.Knight,
                'b' => PieceType.Bishop,
                'r' => PieceType.Rook,
                'q' => PieceType.Queen,
                'k' => PieceType.King,
                _ => throw new ArgumentException(ErrorHelper.InvalidPieceNameSyntax)
            };
            var color = value == char.ToUpper(value) ? Color.White : Color.Black;

            return new PieceFactory().Create(type, color);
        }
    }
}
