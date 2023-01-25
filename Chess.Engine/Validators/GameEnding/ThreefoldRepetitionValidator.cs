using Chess.Engine.Builders;
using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class ThreefoldRepetitionValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .Draw()
                .WithDetails("Draw by threefold repetition")
                .Build();
        }

        public override bool Validate(Game game)
        {
            return game.Turns.Where(
                    x => x.Description == game.LastExecutedTurn?.Description
                ).Count() >= 3;
        }
    }
}
