using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Rook : Piece, IPiece
    {
        public Rook(Color color) : base(color)
        {
            Type = PieceType.Rook;
        }
    }
}
