using Chess.Engine.Builders;
using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public abstract class GameEndingValidator : IGameEndingValidator
    {
        protected GameEndingBuilder GameEndingBuilder = new GameEndingBuilder();

        public virtual GameEnding GetGameEnding(Game game)
        {
            return null;
        }

        public virtual bool Validate(Game game)
        {
            return false;
        }
    }
}
