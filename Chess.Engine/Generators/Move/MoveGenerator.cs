using Chess.Engine.Builders;
using Chess.Engine.Components;

namespace Chess.Engine.Generators
{
    public abstract class MoveGenerator<TPiece> : IMoveGenerator
        where TPiece : class, IPiece
    {
        protected PositionBuilder _positionBuilder;
        protected MoveBuilder _moveBuilder;

        public MoveGenerator()
        {
            _positionBuilder = new PositionBuilder();
            _moveBuilder = new MoveBuilder();
        }

        IList<Move> IMoveGenerator.Generate(IPiece piece)
        {
            return Generate((TPiece)piece);
        }

        public virtual IList<Move> Generate(TPiece piece)
        {
            return new List<Move>();
        }
    }
}
