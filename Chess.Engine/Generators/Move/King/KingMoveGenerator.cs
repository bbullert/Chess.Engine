using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class KingMoveGenerator : MoveGenerator<King>
    {
        public override IList<Move> Generate(King piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GenerateStandard(piece));
            moves.AddRange(GenerateKingSideCastle(piece));
            moves.AddRange(GenerateQueenSideCastle(piece));
            return moves;
        }

        protected IList<Move> GenerateStandard(King piece)
        {
            var moves = new List<Move>();
            
            foreach (var file in new[] { -1, 0, 1 })
            {
                foreach (var rank in new[] { -1, 0, 1 })
                {
                    if (file == 0 && rank == 0) continue;

                    var arrivalSquare = _positionBuilder.For(piece).GetSquare(x => x.Forward(rank).Left(file));
                    if (arrivalSquare is null) continue;

                    var move = _moveBuilder
                        .New()
                        .For(piece)
                        .Type(MoveType.KingStandard)
                        .CaptureMode(CaptureMode.Allow)
                        .ArrivalSquare(arrivalSquare)
                        .Build();

                    moves.Add(move);
                }
            }

            return moves;
        }

        protected IList<Move> GenerateKingSideCastle(King piece)
        {
            var squares = _positionBuilder.For(piece).GetPath(x => x.Right());
            return GenerateCastle(piece, CastleSide.KingSide, squares);
        }

        protected IList<Move> GenerateQueenSideCastle(King piece)
        {
            var squares = _positionBuilder.For(piece).GetPath(x => x.Left());
            return GenerateCastle(piece, CastleSide.QueenSide, squares);
        }

        protected IList<Move> GenerateCastle(King piece, CastleSide castleSide, IEnumerable<Square> path)
        {
            var moves = new List<Move>();

            if (path.Count() < 3)
                return moves;

            if (piece.MoveHistory.Any())
                return moves;

            var firstRankSquare = _positionBuilder.For(piece).GetSquare(x => x.MostBackwardSquare());
            if (piece.Square.Rank != firstRankSquare.Rank)
                return moves;

            var rookSquare = path.Last();
            if (!rookSquare.HasFriend(piece) ||
                !rookSquare.HasType(PieceType.Rook) ||
                rookSquare.Piece.MoveHistory.Any())
                return moves;

            var rookArrivalSquare = path.First();
            var kingArrivalSquare = path.ElementAt(1);

            var castle = new Castle()
            {
                Rook = rookSquare.Piece as Rook,
                CastleSide = castleSide,
                DepartureSquare = rookSquare,
                ArrivalSquare = rookArrivalSquare,
            };
            piece.AddCastlingRight(castle);

            foreach (var square in path.SkipLast(1))
                if (!square.IsEmpty) return moves;

            if (piece.Square.Threats.Any())
                return moves;

            if (rookArrivalSquare.Threats.Any() ||
                kingArrivalSquare.Threats.Any())
                return moves;

            var move = _moveBuilder
                .New()
                .For(piece)
                .Type(MoveType.Castle)
                .CaptureMode(CaptureMode.Forbid)
                .ArrivalSquare(kingArrivalSquare)
                .Castle(castle)
                .Build();

            moves.Add(move);

            return moves;
        }
    }
}
