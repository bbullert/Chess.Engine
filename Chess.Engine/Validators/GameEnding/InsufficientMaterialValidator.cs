using Chess.Engine.Builders;
using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Validators
{
    public class InsufficientMaterialValidator : GameEndingValidator
    {
        public override GameEnding GetGameEnding(Game game)
        {
            return GameEndingBuilder
                .New()
                .Draw()
                .WithDetails("Draw by insufficient material")
                .Build();
        }

        public override bool Validate(Game game)
        {
            return HasInsufficientMatingMaterial(game.ActivePlayer.Pieces)
                && HasInsufficientMatingMaterial(game.PassivePlayer.Pieces);
        }

        protected bool HasInsufficientMatingMaterial(IEnumerable<IPiece> pieces)
        {
            pieces = pieces.Where(x => x.Type != PieceType.King).ToList();

            if (pieces.Any(x => x.Type == PieceType.Pawn))
                return false;
            if (pieces.Any(x => x.Type == PieceType.Rook))
                return false;
            if (pieces.Any(x => x.Type == PieceType.Queen))
                return false;
            if (pieces.Any(x => x.Type == PieceType.Knight) &&
                pieces.Any(x => x.Type == PieceType.Bishop))
                return false;
            if (pieces.Any(x => x.Type == PieceType.Bishop && x.Square.Color == Color.White) &&
                pieces.Any(x => x.Type == PieceType.Bishop && x.Square.Color == Color.Black))
                return false;

            return true;
        }
    }
}
