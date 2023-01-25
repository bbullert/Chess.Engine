using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Pawn : Piece, IPiece
    {
        public Pawn(Color color) : base(color)
        {
            Type = PieceType.Pawn;
        }

        public void Promote(PieceType toType)
        {
            if (toType == PieceType.Pawn ||
                toType == PieceType.King)
                throw new InvalidOperationException();
            Type = toType;
        }
    }
}
