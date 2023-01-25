using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Components
{
    public class Square : IEquatable<Square>
    {
        public Square(int file, int rank)
        {
            File = file;
            Rank = rank;
            Threats = new List<Move>();
            Claims = new List<Move>();
        }

        public Square(Square square) : this(square.File, square.Rank)
        {
        }

        public int File { get; set; }
        public string FileName => File.ToFileName();
        public int Rank { get; set; }
        public string RankName => Rank.ToString();
        public Color Color { get; set; }
        public IPiece Piece { get; set; }
        public ChessBoard ChessBoard { get; set; }
        public IList<Move> Threats { get; set; }
        public IList<Move> Claims { get; set; }
        public bool IsEmpty => Piece == null;

        public bool HasFriend(IPiece piece)
        {
            return !IsEmpty && Piece.Color == piece.Color;
        }

        public bool HasEnemy(IPiece piece)
        {
            return !IsEmpty && Piece.Color != piece.Color;
        }

        public bool HasType(PieceType type)
        {
            return !IsEmpty && Piece.Type == type;
        }

        public void AddThreat(Move move)
        {
            Threats.Add(move);
        }

        public void AddClaim(Move move)
        {
            Claims.Add(move);
        }

        public void Assign(IPiece piece)
        {
            Piece = piece;
            Piece.Square = this;
        }

        public void Release()
        {
            Piece.Square = null;
            Piece = null;
        }

        public override string ToString()
        {
            return FileName + RankName;
        }

        public bool Equals(Square? other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType()) return false;
            if (File != other.File || Rank != other.Rank) return false;

            return true;
        }

        public override int GetHashCode() => (File, Rank).GetHashCode();

        public static bool operator == (Square left, Square right)
        {
            if (left is null) return right is null;

            return left.Equals(right);
        }

        public static bool operator != (Square left, Square right) => !(left == right);
    }
}
