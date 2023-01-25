using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public abstract class ContinuousMoveGenerator<TPiece> : MoveGenerator<TPiece>
        where TPiece : class, IPiece
    {
        protected IList<Move> Generate(TPiece piece, MoveType moveType, IEnumerable<Square> path)
        {
            var moves = new List<Move>();

            foreach (var (square, index) in path.Select((square, index) => (square, index)))
            {
                var move = _moveBuilder
                    .New()
                    .For(piece)
                    .Type(moveType)
                    .CaptureMode(CaptureMode.Allow)
                    .ArrivalSquare(square)
                    .Path(path)
                    .Build();

                if (square.HasEnemy(piece) && !square.HasType(PieceType.King))
                    SolvePin(square.Piece, move, path.Skip(index + 1));

                moves.Add(move);

                if (!square.IsEmpty) break;
            }

            return moves;
        }

        protected void SolvePin(IPiece piece, Move move, IEnumerable<Square> possibleKingSquares)
        {
            foreach (var square in possibleKingSquares)
            {
                if (square.HasFriend(piece) &&
                    square.HasType(PieceType.King))
                    piece.PinnedBy = move;

                if (!square.IsEmpty) break;
            }
        }
    }
}
