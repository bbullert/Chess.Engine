using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Queen : Piece, IPiece
    {
        public Queen(Color color) : base(color)
        {
            Type = PieceType.Queen;
        }
    }
}
