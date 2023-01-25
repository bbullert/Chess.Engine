using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class PawnMoveGenerator : MoveGenerator<Pawn>
    {
        public override IList<Move> Generate(Pawn piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GenerateSingleForward(piece));
            moves.AddRange(GenerateDoubleForward(piece));
            moves.AddRange(GenerateSingleForwardDiagonal(piece));
            moves.AddRange(GenerateEnPassant(piece));
            return moves;
        }

        protected IList<Move> GenerateSingleForward(Pawn piece)
        {
            var moves = new List<Move>();

            var arrivalSquare = _positionBuilder.For(piece).GetSquare(x => x.Forward());
            if (arrivalSquare is null) return moves;

            var lastSquare = _positionBuilder.For(piece).GetSquare(x => x.MostForwardSquare());

            var move = _moveBuilder
                .New()
                .For(piece)
                .Type(MoveType.SingleForward)
                .CaptureMode(CaptureMode.Forbid)
                .ArrivalSquare(arrivalSquare)
                .HasPromotion(arrivalSquare.Rank == lastSquare.Rank)
                .Build();

            moves.Add(move);

            return moves;
        }

        protected IList<Move> GenerateDoubleForward(Pawn piece)
        {
            var moves = new List<Move>();

            if (piece.MoveHistory.Any())
                return moves;

            var secondRankSquare = _positionBuilder.For(piece).GetSquare(x => x.MostBackwardSquare().Forward());
            if (piece.Square.Rank != secondRankSquare.Rank)
                return moves;

            var path = _positionBuilder.For(piece).GetPath(x => x.Forward(), 2);
            if (path.Count() != 2 || !path.First().IsEmpty)
                return moves;

            var lastSquare = _positionBuilder.For(piece).GetSquare(x => x.MostForwardSquare());

            var move = _moveBuilder
                .New()
                .For(piece)
                .Type(MoveType.DoubleForward)
                .CaptureMode(CaptureMode.Forbid)
                .ArrivalSquare(path.Last())
                .EnPassantSquare(path.First())
                .HasPromotion(path.Last().Rank == lastSquare.Rank)
                .Build();

            moves.Add(move);

            return moves;
        }

        protected IList<Move> GenerateSingleForwardDiagonal(Pawn piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var arrivalSquare = _positionBuilder.For(piece).GetSquare(x => x.Forward().Left(file));
                if (arrivalSquare is null) continue;

                var lastSquare = _positionBuilder.For(piece).GetSquare(x => x.MostForwardSquare());

                var move = _moveBuilder
                    .New()
                    .For(piece)
                    .Type(MoveType.SingleForwardDiagonal)
                    .CaptureMode(CaptureMode.Require)
                    .ArrivalSquare(arrivalSquare)
                    .HasPromotion(arrivalSquare.Rank == lastSquare.Rank)
                    .Build();

                moves.Add(move);
            }

            return moves;
        }

        protected IList<Move> GenerateEnPassant(Pawn piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var enemySquare = _positionBuilder.For(piece).GetSquare(x => x.Left(file));
                if (enemySquare is null ||
                    !enemySquare.HasEnemy(piece) ||
                    !enemySquare.HasType(PieceType.Pawn)) continue;

                var enemyLastMove = enemySquare.Piece?.Player.MoveHistory.LastOrDefault();
                if (enemyLastMove is null ||
                    enemyLastMove.ArrivalSquare != enemySquare ||
                    enemyLastMove.Type != MoveType.DoubleForward) continue;

                var arrivalSquare = _positionBuilder.For(piece).GetSquare(x => x.Forward().Left(file));
                if (arrivalSquare is null ||
                    !arrivalSquare.IsEmpty) continue;

                var lastSquare = _positionBuilder.For(piece).GetSquare(x => x.MostForwardSquare());

                var move = _moveBuilder
                    .New()
                    .For(piece)
                    .Type(MoveType.EnPassant)
                    .CaptureMode(CaptureMode.Require)
                    .ArrivalSquare(arrivalSquare)
                    .Capture(new Capture()
                    {
                        CaptureSquare = enemySquare
                    })
                    .HasPromotion(arrivalSquare.Rank == lastSquare.Rank)
                    .Build();

                moves.Add(move);
            }

            return moves;
        }
    }
}
