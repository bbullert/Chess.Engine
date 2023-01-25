using Chess.Engine.Components;
using Chess.Engine.Enums;
using Chess.Engine.Validators;

namespace Chess.Engine.Resolvers
{
    public class MoveResolver
    {
        private readonly MoveValidator _moveValidator = new MoveValidator();
        private readonly KingMoveValidator _kingMoveValidator = new KingMoveValidator();

        public void Solve(Player player)
        {
            var king = player.Pieces.FirstOrDefault(
                    x => x.Type == PieceType.King
                ) as King;
            if (king is null)
                throw new ArgumentNullException("King is required");

            foreach (var piece in player.Pieces)
            {
                foreach (var candidate in piece.MoveCandidates)
                {
                    if (piece.Type == PieceType.King)
                    {
                        if (_kingMoveValidator.Validate(candidate, king))
                            piece.AddMove(candidate);
                    }
                    else
                    {
                        if (_moveValidator.Validate(candidate, king))
                            piece.AddMove(candidate);
                    }
                }
            }
        }
    }
}
