using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Knight : Piece, IPiece
    {
        public Knight(Color color) : base(color)
        {
            Type = PieceType.Knight;
        }
    }
}
