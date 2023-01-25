using Chess.Engine.Components;

namespace Chess.Engine.Builders
{
    public class PositionBuilder
    {
        private IPiece _piece;

        private ChessBoard ChessBoard => _piece?.Square?.ChessBoard;
        private Square DepartureSquare { get; set; }
        private Square DestinationSquare { get; set; }

        public PositionBuilder For(IPiece piece)
        {
            if (piece is null)
                throw new ArgumentNullException();
            _piece = piece;

            DepartureSquare = new Square(_piece.Square);
            DestinationSquare = new Square(_piece.Square);

            return this;
        }

        public Square GetSquare(Func<PositionBuilder, PositionBuilder> move)
        {
            if (ChessBoard == null)
                throw new ArgumentNullException();

            move(this);

            return ChessBoard.FindSquare(DestinationSquare.ToString());
        }

        public IEnumerable<Square> GetPath(Func<PositionBuilder, PositionBuilder> move, int? pathLenght = null)
        {
            if (ChessBoard == null)
                throw new ArgumentNullException();

            if (pathLenght == null)
                pathLenght = Math.Max(ChessBoard.FileCount, ChessBoard.RankCount);

            var squares = new List<Square>();
            for (int i = 0; i < pathLenght; i++)
            {
                move(this);

                var square = ChessBoard.FindSquare(DestinationSquare.ToString());
                if (square == null) break;

                squares.Add(square);
            }

            return squares;
        }

        public PositionBuilder Forward(int range = 1)
        {
            DestinationSquare.Rank += _piece.DirectionModifier * range;
            return this;
        }

        public PositionBuilder Backward(int range = 1)
        {
            DestinationSquare.Rank -= _piece.DirectionModifier * range;
            return this;
        }

        public PositionBuilder Left(int range = 1)
        {
            DestinationSquare.File -= _piece.DirectionModifier * range;
            return this;
        }

        public PositionBuilder Right(int range = 1)
        {
            DestinationSquare.File += _piece.DirectionModifier * range;
            return this;
        }

        public PositionBuilder MostForwardSquare()
        {
            DestinationSquare.Rank = _piece.DirectionModifier < 0 
                ? 1 : ChessBoard.RankCount;
            return this;
        }

        public PositionBuilder MostBackwardSquare()
        {
            DestinationSquare.Rank = _piece.DirectionModifier > 0 
                ? 1 : ChessBoard.RankCount;
            return this;
        }

        public PositionBuilder MostLeftSquare()
        {
            DestinationSquare.File = _piece.DirectionModifier > 0 
                ? 1 : ChessBoard.FileCount;
            return this;
        }

        public PositionBuilder MostRightSquare()
        {
            DestinationSquare.File = _piece.DirectionModifier < 0 
                ? 1 : ChessBoard.FileCount;
            return this;
        }
    }
}
