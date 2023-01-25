using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Factories
{
    public class PieceFactory
    {
        public static readonly IDictionary<PieceType, Func<Color, IPiece>> Creators =
            new Dictionary<PieceType, Func<Color, IPiece>>()
            {
                { PieceType.Pawn, (Color color) => new Pawn(color) },
                { PieceType.Knight, (Color color) => new Knight(color) },
                { PieceType.Bishop, (Color color) => new Bishop(color) },
                { PieceType.Rook, (Color color) => new Rook(color) },
                { PieceType.Queen, (Color color) => new Queen(color) },
                { PieceType.King, (Color color) => new King(color) },
            };

        public IPiece Create(PieceType pieceType, Color color)
        {
            return Creators[pieceType](color);
        }
    }
}
