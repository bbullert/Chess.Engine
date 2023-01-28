using Chess.Engine.Components;
using Chess.Engine.Enums;

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
