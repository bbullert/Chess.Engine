using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class TimeoutValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .LossFor(game.ActivePlayer)
                .WinFor(game.PassivePlayer)
                .WithDetails("{Loser} lose by timeout")
                .Build();
        }

        public override bool Validate(Game game)
        {
            return game.GameSetup.TimeLimit.HasValue
                && game.ActiveTurn.DateTimeFrom.Add(game.ActiveTurn.Duration)
                >= game.ActivePlayer.Timer.Timeout;
        }
    }
}
