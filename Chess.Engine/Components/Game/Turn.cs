using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Components
{
    public class Turn
    {
        public int TurnClock { get; set; }
        public Color Color { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
        public TimeSpan Duration => (DateTimeTo ?? DateTime.Now) - DateTimeFrom;
        public GameState GameState { get; set; }
        public Move Move { get; set; }
        public bool IsExcecuted => DateTimeTo is not null;

        public void Initialize(Game game)
        {
            if (IsExcecuted)
            {
                Description = this.ToShortFeNotation();
                Move.Initialize(game);
            }
        }
    }
}
