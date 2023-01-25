using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public interface IGameEndingValidator
    {
        GameEnding GetGameEnding(Game game);
        bool Validate(Game game);
    }
}
