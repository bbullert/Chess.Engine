using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Bishop : Piece, IPiece
    {
        public Bishop(Color color) : base(color)
        {
            Type = PieceType.Bishop;
        }
    }
}
