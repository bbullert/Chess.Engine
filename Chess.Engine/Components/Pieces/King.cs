using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class King : Piece, IPiece
    {
        public King(Color color) : base(color)
        {
            Type = PieceType.King;
            CastlingRights = new List<Castle>();
        }

        public IList<Castle> CastlingRights { get; set; }
        public IEnumerable<Move> Checks => Square.Threats;
        public bool IsInCheck => Checks.Any();

        public void AddCastlingRight(Castle castle)
        {
            CastlingRights = CastlingRights.Append(castle).ToList();
        }
    }
}
