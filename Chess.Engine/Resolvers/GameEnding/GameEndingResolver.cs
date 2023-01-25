using Chess.Engine.Components;
using Chess.Engine.Validators;

namespace Chess.Engine.Resolvers
{
    public class GameEndingResolver
    {
        protected readonly IEnumerable<IGameEndingValidator> Validators =
            new List<IGameEndingValidator>()
            {
                new CheckmateValidator(),
                new FiftyMoveRuleValidator(),
                new InsufficientMaterialValidator(),
                new StalemateValidator(),
                new ThreefoldRepetitionValidator(),
                new TimeoutValidator(),
            };

        public void Solve(Game game)
        {
            foreach (var rule in Validators)
            {
                if (rule.Validate(game))
                    game.GameEnding = rule.GetGameEnding(game);
            }
        }
    }
}
