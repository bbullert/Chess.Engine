using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public abstract class Piece : IPiece
    {
        public Piece(Color color)
        {
            Color = color;
            MoveHistory = new List<Move>();
            MoveCandidates = new List<Move>();
            Moves = new List<Move>();
        }

        public Color Color { get; set; }
        public int DirectionModifier => Color == Color.White ? 1 : -1;
        public PieceType Type { get; set; }
        public Player Player { get; set; }
        public Square Square { get; set; }
        public IList<Move> MoveHistory { get; set; }
        public IList<Move> MoveCandidates { get; set; }
        public IList<Move> Moves { get; set; }
        public Move PinnedBy { get; set; }
        public bool IsPinned => PinnedBy is not null;
        public bool IsCaptured => Square is null;

        public bool IsFriend(IPiece piece)
        {
            return Color == piece.Color;
        }

        public bool IsEnemy(IPiece piece)
        {
            return Color != piece.Color;
        }

        public void Initialize(Game game)
        {
            Player = game.Players.First(x => x.Color == Color);
        }

        public void PrependMoveHistory(Move move)
        {
            move.Piece = this;
            MoveHistory.Insert(0, move);
        }

        public void AddMoveCandidate(Move move)
        {
            MoveCandidates.Add(move);
        }

        public void AddMove(Move move)
        {
            Moves.Add(move);
            move.ArrivalSquare.AddClaim(move);
        }

        public void Move(Square arrivalSquare)
        {
            Square.Release();
            arrivalSquare.Assign(this);
        }

        public void Capture(IPiece piece)
        {
            piece.Square.Release();
            piece.Player.LosePiece(piece);
        }
    }
}
