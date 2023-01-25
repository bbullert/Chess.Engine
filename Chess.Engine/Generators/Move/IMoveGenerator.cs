using Chess.Engine.Components;

namespace Chess.Engine.Generators
{
    public interface IMoveGenerator
    {
        IList<Move> Generate(IPiece piece);
    }
}
