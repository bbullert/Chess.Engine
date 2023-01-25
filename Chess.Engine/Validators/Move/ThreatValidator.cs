using Chess.Engine.Enums;
using Chess.Engine.Components;

namespace Chess.Engine.Validators
{
    public class ThreatValidator
    {
        public bool Validate(Move move)
        {
            if (move.CaptureMode == CaptureMode.Forbid)
                return false;

            return true;
        }
    }
}
