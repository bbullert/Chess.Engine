using Chess.Engine.Components;
using Chess.Engine.Validators;

namespace Chess.Engine.Resolvers
{
    public class MoveCandidateResolver : PlayerResolver
    {
        private readonly MoveCandidateValidator _moveCandidateValidator = new MoveCandidateValidator();

        public void Solve(Player player)
        {
            foreach (var piece in player.Pieces)
            {
                var candidates = Generators[piece.Type].Generate(piece);
                foreach (var candidate in candidates)
                {
                    if (_moveCandidateValidator.Validate(candidate))
                        piece.AddMoveCandidate(candidate);
                }
            }
        }
    }
}
