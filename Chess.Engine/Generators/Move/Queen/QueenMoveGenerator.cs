using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class QueenMoveGenerator : ContinuousMoveGenerator<Queen>
    {
        public override IList<Move> Generate(Queen piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GenerateHorizontal(piece));
            moves.AddRange(GenerateVertical(piece));
            moves.AddRange(GenerateDiagonals(piece));
            return moves;
        }

        protected IList<Move> GenerateHorizontal(Queen piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var squares = _positionBuilder.For(piece).GetPath(x => x.Left(file));
                moves.AddRange(base.Generate(piece, MoveType.ContinuousHorizontal, squares));
            }

            return moves;
        }

        protected IList<Move> GenerateVertical(Queen piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var squares = _positionBuilder.For(piece).GetPath(x => x.Forward(file));
                moves.AddRange(base.Generate(piece, MoveType.ContinuousVertical, squares));
            }

            return moves;
        }

        protected IEnumerable<Move> GenerateDiagonals(Queen piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                foreach (var rank in new[] { -1, 1 })
                {
                    var squares = _positionBuilder.For(piece).GetPath(x => x.Forward(rank).Left(file));
                    moves.AddRange(base.Generate(piece, MoveType.ContinuousDiagonal, squares));
                }
            }

            return moves;
        }
    }
}
