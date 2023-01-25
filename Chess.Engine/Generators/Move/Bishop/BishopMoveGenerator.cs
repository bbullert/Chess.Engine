using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class BishopMoveGenerator : ContinuousMoveGenerator<Bishop>
    {
        public override IList<Move> Generate(Bishop piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GenerateDiagonals(piece));
            return moves;
        }

        protected IList<Move> GenerateDiagonals(Bishop piece)
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
