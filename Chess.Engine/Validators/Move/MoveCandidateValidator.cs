using Chess.Engine.Enums;
using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class MoveCandidateValidator
    {
        public bool Validate(Move move)
        {
            if (move.CaptureMode == CaptureMode.Forbid &&
                    move.Capture is not null)
                return false;

            if (move.CaptureMode == CaptureMode.Require &&
                move.Capture is null)
                return false;

            if (move.ArrivalSquare.HasFriend(move.Piece))
                return false;

            return true;
        }
    }
}
