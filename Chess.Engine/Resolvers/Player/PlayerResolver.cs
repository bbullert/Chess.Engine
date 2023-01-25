using Chess.Engine.Enums;
using Chess.Engine.Generators;

namespace Chess.Engine.Resolvers
{
    public class PlayerResolver
    {
        protected readonly IDictionary<PieceType, IMoveGenerator> Generators =
            new Dictionary<PieceType, IMoveGenerator>()
            {
                { PieceType.Pawn, new PawnMoveGenerator() },
                { PieceType.Knight, new KnightMoveGenerator() },
                { PieceType.Bishop, new BishopMoveGenerator() },
                { PieceType.Rook, new RookMoveGenerator() },
                { PieceType.Queen, new QueenMoveGenerator() },
                { PieceType.King, new KingMoveGenerator() },
            };
    }
}
