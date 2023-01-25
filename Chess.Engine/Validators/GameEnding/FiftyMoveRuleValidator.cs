using Chess.Engine.Builders;
using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class FiftyMoveRuleValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .Draw()
                .WithDetails("Draw by 50 move rule")
                .Build();
        }

        public override bool Validate(Game game)
        {
            return (game.LastExecutedTurn?.GameState.FiftyMoveRuleClock ?? 0) >= 99;
        }
    }
}
