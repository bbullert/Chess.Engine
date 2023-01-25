using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class KingMoveValidator
    {
        public bool Validate(Move move, King king)
        {
            if (move.ArrivalSquare.Threats.Any())
                return false;

            foreach (var square in king.Checks.SelectMany(x => x.Path))
            {
                if (move.ArrivalSquare == square)
                    return false;
            }

            return true;
        }
    }
}
