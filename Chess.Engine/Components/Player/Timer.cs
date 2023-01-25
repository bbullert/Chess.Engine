namespace Chess.Engine.Components
{
    public class Timer
    {
        public TimeSpan TimeElapsed { get; set; }
        public TimeSpan? TimeLeft { get; set; }
        public DateTime? Timeout { get; set; }
        public bool IsEnabled { get; set; }

        public void Initialize(Game game, Player player)
        {
            IsEnabled = !game.HasEnded && game.ActivePlayer.Color == player.Color;

            TimeElapsed = TimeSpan.Zero;
            foreach (var turn in player.ExecutedTurns)
                TimeElapsed += turn.Duration;

            var timeLimit = TimeSpan.FromMinutes(game.GameSetup.TimeLimit.Value);
            var timeIncrement = TimeSpan.FromSeconds(game.GameSetup.TimeIncrement);

            TimeLeft = timeLimit - TimeElapsed +
                timeIncrement * player.ExecutedTurns.Count();
            if (TimeLeft.Value.Ticks < 0)
                TimeLeft = TimeSpan.Zero;
            if (player.Turns.Any())
                Timeout = player.Turns.Last().DateTimeFrom.Add(TimeLeft.Value);
        }
    }
}
