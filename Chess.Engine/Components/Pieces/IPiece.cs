using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public interface IPiece
    {
        Color Color { get; set; }
        int DirectionModifier { get; }
        PieceType Type { get; set; }
        Player Player { get; set; }
        Square Square { get; set; }
        IList<Move> MoveHistory { get; set; }
        IList<Move> MoveCandidates { get; set; }
        IList<Move> Moves { get; set; }
        Move PinnedBy { get; set; }
        bool IsPinned { get; }
        bool IsCaptured { get; }
        bool IsFriend(IPiece piece);
        bool IsEnemy(IPiece piece);
        void Initialize(Game game);
        void PrependMoveHistory(Move move);
        void AddMoveCandidate(Move move);
        void AddMove(Move move);
        void Move(Square arrivalSquare);
        void Capture(IPiece piece);
    }
}
