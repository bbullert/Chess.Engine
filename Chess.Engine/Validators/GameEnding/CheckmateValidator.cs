using Chess.Engine.Builders;
using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Validators
{
    public class CheckmateValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .LossFor(game.ActivePlayer)
                .WinFor(game.PassivePlayer)
                .WithDetails("{Winner} win by checkmate")
                .Build();
        }

        public override bool Validate(Game game)
        {
            var king = game.ActivePlayer.Pieces.FirstOrDefault(
                    x => x.Type == PieceType.King
                ) as King;
            if (king is null)
                throw new ArgumentNullException("King is required");

            return king.IsInCheck
                && !game.ActivePlayer.Pieces.SelectMany(x => x.Moves).Any();
        }
    }
}
