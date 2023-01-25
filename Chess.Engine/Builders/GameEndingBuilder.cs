using Chess.Engine.Components;
using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Builders
{
    public class GameEndingBuilder
    {
        private GameEnding _gameEnding;

        public GameEndingBuilder New()
        {
            _gameEnding = new GameEnding();
            return this;
        }

        public GameEndingBuilder WinFor(Player player)
        {
            _gameEnding.Winner = player;
            _gameEnding.StrongName = player.Color == Color.White
                ? "1-0" : "0-1";
            return this;
        }

        public GameEndingBuilder LossFor(Player player)
        {
            _gameEnding.Loser = player;
            _gameEnding.StrongName = player.Color == Color.White
                ? "0-1" : "1-0";
            return this;
        }

        public GameEndingBuilder Draw()
        {
            _gameEnding.Winner = null;
            _gameEnding.Loser = null;
            _gameEnding.StrongName = "½-½";
            return this;
        }

        public GameEndingBuilder WithDetails(string details)
        {
            _gameEnding.Details = details;
            if (_gameEnding.Winner is not null)
                _gameEnding.Details = _gameEnding.Details
                    .Replace("{Winner}", _gameEnding.Winner.GetName());
            if (_gameEnding.Loser is not null)
                _gameEnding.Details = _gameEnding.Details
                    .Replace("{Loser}", _gameEnding.Loser.GetName());
            return this;
        }

        public GameEnding Build()
        {
            return _gameEnding;
        }
    }
}
