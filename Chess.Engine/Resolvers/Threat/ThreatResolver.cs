using Chess.Engine.Components;
using Chess.Engine.Validators;

namespace Chess.Engine.Resolvers
{
    public class ThreatResolver : PlayerResolver
    {
        private readonly ThreatValidator _threatValidator = new ThreatValidator();

        public void Solve(Player player)
        {
            foreach (var piece in player.Pieces)
            {
                var threats = Generators[piece.Type].Generate(piece);
                foreach (var threat in threats)
                {
                    var square = threat.Capture?.CaptureSquare ?? threat.ArrivalSquare;
                    if (_threatValidator.Validate(threat))
                        square.AddThreat(threat);
                }
            }
        }
    }
}
