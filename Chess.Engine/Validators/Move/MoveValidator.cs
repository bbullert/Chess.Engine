using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class MoveValidator
    {
        public bool Validate(Move move, King king)
        {
            if (!king.IsInCheck)
            {
                if (!move.Piece.IsPinned) return true;

                if (move.Capture?.CaptureSquare == move.Piece.PinnedBy.DepartureSquare)
                    return true;

                var path = move.Piece.PinnedBy.Path ?? Enumerable.Empty<Square>();
                foreach (var square in path)
                {
                    if (move.ArrivalSquare == square)
                        return true;
                }
            }
            else if (king.Checks.Count() == 1)
            {
                if (move.Capture?.CaptureSquare == king.Checks.First().DepartureSquare)
                    return true;

                var path = king.Checks.First().Path ?? Enumerable.Empty<Square>();
                foreach (var square in path)
                {
                    if (move.ArrivalSquare == square)
                        return true;
                }
            }

            return false;
        }
    }
}
