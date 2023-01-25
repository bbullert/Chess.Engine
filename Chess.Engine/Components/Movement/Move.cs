using Chess.Engine.Enums;

namespace Chess.Engine.Components
{
    public class Move
    {
        public MoveType Type { get; set; }
        public IPiece Piece { get; set; }
        public string Description { get; set; }
        public Square DepartureSquare { get; set; }
        public Square ArrivalSquare { get; set; }
        public Square EnPassantSquare { get; set; }
        public CaptureMode CaptureMode { get; set; }
        public Capture Capture { get; set; }
        public Promotion Promotion { get; set; }
        public Castle Castle { get; set; }
        public IEnumerable<Square> Path { get; set; }

        public void Initialize(Game game)
        {
            DepartureSquare = game.ChessBoard.FindSquare(DepartureSquare.ToString());
            ArrivalSquare = game.ChessBoard.FindSquare(ArrivalSquare.ToString());
            EnPassantSquare = game.ChessBoard.FindSquare(ArrivalSquare.ToString());
        }

        public override string ToString()
        {
            return DepartureSquare.ToString() + ArrivalSquare.ToString();
        }
    }
}
