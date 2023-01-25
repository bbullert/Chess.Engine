using Chess.Engine.Builders;
using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Validators
{
    public class StalemateValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .Draw()
                .WithDetails("Draw by stalemate")
                .Build();
        }

        public override bool Validate(Game game)
        {
            var king = game.ActivePlayer.Pieces.FirstOrDefault(
                    x => x.Type == PieceType.King
                ) as King;
            if (king is null)
                throw new ArgumentNullException("King is required");

            return !king.IsInCheck
                && !game.ActivePlayer.Pieces.SelectMany(x => x.Moves).Any();
        }
    }
}
