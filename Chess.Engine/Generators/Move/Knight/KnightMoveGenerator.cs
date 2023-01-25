using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Generators
{
    public class KnightMoveGenerator : MoveGenerator<Knight>
    {
        public override IList<Move> Generate(Knight piece)
        {
            var moves = new List<Move>();

            foreach (var i in new[] { 1, 2 })
            {
                foreach (var j in new[] { -1, 1 })
                {
                    foreach (var k in new[] { -1, 1 })
                    {
                        int file = i * j,
                            rank = (3 - i) * k;

                        var arrivalSquare = _positionBuilder.For(piece).GetSquare(x => x.Forward(rank).Right(file));
                        if (arrivalSquare is null) continue;

                        var move = _moveBuilder
                            .New()
                            .For(piece)
                            .Type(MoveType.KnightStandard)
                            .CaptureMode(CaptureMode.Allow)
                            .ArrivalSquare(arrivalSquare)
                            .Build();

                        moves.Add(move);
                    }
                }
            }

            return moves;
        }
    }
}
