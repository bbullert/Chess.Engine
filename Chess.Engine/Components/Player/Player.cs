using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Components
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public IList<Turn> Turns { get; set; }
        public IEnumerable<Turn> ExecutedTurns => Turns?.Where(x => x.IsExcecuted) ?? Enumerable.Empty<Turn>();
        public IList<Move> MoveHistory { get; set; }
        public IList<IPiece> Pieces { get; set; }
        public IList<IPiece> LostPieces { get; set; }
        public Timer Timer { get; set; }

        public Player()
        {
            Turns = new List<Turn>();
            MoveHistory = new List<Move>();
            Pieces = new List<IPiece>();
            LostPieces = new List<IPiece>();
        }

        public void Initialize(Game game)
        {
            Turns = game.Turns.Where(x => x.Color == Color).ToList();

            var capturedPiecesString = game.ExecutedTurns.LastOrDefault()?.GameState.CapturedPieces ?? string.Empty;
            var lostPieces = capturedPiecesString
                .ToCharArray()
                .Select(x => x.ToPiece())
                .Where(x => x.Color == Color)
                .ToList();
            foreach (var piece in game.ChessBoard.Pieces.Where(x => x.Color == Color))
            {
                AddPiece(piece);
            }
            foreach (var piece in lostPieces)
            {
                AddLostPiece(piece);
            }

            foreach (var turn in ExecutedTurns.Reverse())
            {
                foreach (var piece in Pieces)
                {
                    var pieceLastSquare =
                        piece.MoveHistory.FirstOrDefault()?.DepartureSquare ??
                        piece.Square;
                    if (pieceLastSquare != turn.Move.ArrivalSquare)
                        continue;

                    piece.PrependMoveHistory(turn.Move);
                    PrependMoveHistory(turn.Move);
                    break;
                }
            }

            if (game.GameSetup.TimeLimit.HasValue)
            {
                Timer = new Timer();
                Timer.Initialize(game, this);
            }
        }

        public void PrependMoveHistory(Move move)
        {
            MoveHistory.Insert(0, move);
        }

        public void AddPiece(IPiece piece)
        {
            piece.Player = this;
            Pieces.Add(piece);
        }

        public void AddLostPiece(IPiece piece)
        {
            piece.Player = this;
            LostPieces.Add(piece);
        }

        public void LosePiece(IPiece piece)
        {
            Pieces.Remove(piece);
            LostPieces.Add(piece);
        }
    }
}
