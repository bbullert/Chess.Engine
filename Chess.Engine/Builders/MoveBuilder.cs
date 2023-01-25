using Chess.Engine.Components;
using Chess.Engine.Enums;

namespace Chess.Engine.Builders
{
    public class MoveBuilder
    {
        private Move _move;

        public MoveBuilder New()
        {
            _move = new Move();
            return this;
        }

        public MoveBuilder For(IPiece piece)
        {
            _move.Piece = piece;
            return this;
        }

        public MoveBuilder Type(MoveType type)
        {
            _move.Type = type;
            return this;
        }

        public MoveBuilder Description(string description)
        {
            _move.Description = description;
            return this;
        }

        public MoveBuilder DepartureSquare(Square departureSquare)
        {
            _move.DepartureSquare = departureSquare;
            return this;
        }

        public MoveBuilder ArrivalSquare(Square arrivalSquare)
        {
            _move.ArrivalSquare = arrivalSquare;
            return this;
        }

        public MoveBuilder EnPassantSquare(Square enPassantSquare)
        {
            _move.EnPassantSquare = enPassantSquare;
            return this;
        }

        public MoveBuilder CaptureMode(CaptureMode captureMode)
        {
            _move.CaptureMode = captureMode;
            return this;
        }

        public MoveBuilder Capture(Capture capture)
        {
            _move.Capture = capture;
            return this;
        }

        public MoveBuilder HasPromotion(bool value)
        {
            if (value) _move.Promotion = new Promotion();
            return this;
        }

        public MoveBuilder Castle(Castle castle)
        {
            _move.Castle = castle;
            return this;
        }

        public MoveBuilder Path(IEnumerable<Square> path)
        {
            _move.Path = path;
            return this;
        }

        public Move Build()
        {
            if (_move.Piece == null)
                throw new ArgumentNullException();

            _move.DepartureSquare ??= _move.Piece.Square;

            if (_move.ArrivalSquare is null)
                throw new ArgumentNullException();

            if (_move.Capture?.CaptureSquare is null)
            {
                if (_move.ArrivalSquare.HasEnemy(_move.Piece))
                {
                    _move.Capture ??= new Capture();
                    _move.Capture.CaptureSquare = _move.ArrivalSquare;
                }
            }
            else
            {
                if (!_move.Capture.CaptureSquare.HasEnemy(_move.Piece))
                    _move.Capture = null;
            }

            return _move;
        }
    }
}
