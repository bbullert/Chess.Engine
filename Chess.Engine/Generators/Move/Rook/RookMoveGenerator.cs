using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class RookMoveGenerator : ContinuousMoveGenerator<Rook>
    {
        public override IList<Move> Generate(Rook piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GenerateHorizontal(piece));
            moves.AddRange(GenerateVertical(piece));
            return moves;
        }

        protected IList<Move> GenerateHorizontal(Rook piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var squares = _positionBuilder.For(piece).GetPath(x => x.Left(file));
                moves.AddRange(base.Generate(piece, MoveType.ContinuousHorizontal, squares));
            }

            return moves;
        }

        protected IList<Move> GenerateVertical(Rook piece)
        {
            var moves = new List<Move>();

            foreach (var file in new[] { -1, 1 })
            {
                var squares = _positionBuilder.For(piece).GetPath(x => x.Forward(file));
                moves.AddRange(base.Generate(piece, MoveType.ContinuousVertical, squares));
            }

            return moves;
        }
    }
}
