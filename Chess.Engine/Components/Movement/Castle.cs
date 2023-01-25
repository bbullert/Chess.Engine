using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Castle
    {
        public Rook Rook { get; set; }
        public CastleSide CastleSide { get; set; }
        public Square DepartureSquare { get; set; }
        public Square ArrivalSquare { get; set; }
    }
}
