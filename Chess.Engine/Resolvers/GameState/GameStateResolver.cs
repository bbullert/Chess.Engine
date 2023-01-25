using Chess.Engine.Components;
using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Resolvers
{
    public class GameStateResolver
    {
        public void Solve(Game game)
        {
            var gameState = new GameState();
            
            gameState.ChessBoardState = game.ChessBoard.ToFeNotation();
            gameState.CastlingRights = SolveCastlingRight(game);

            gameState.EnPassantSquareName =
                game.ActiveTurn.Move.EnPassantSquare is not null
                ? game.ActiveTurn.Move.EnPassantSquare.ToString()
                : null;

            gameState.FiftyMoveRuleClock =
                game.ActiveTurn.Move.Piece.Type == PieceType.Pawn ||
                game.ActiveTurn.Move.Capture is not null ?
                0 : (game.LastExecutedTurn?.GameState.FiftyMoveRuleClock ?? 0) + 1;

            var capturedPieces = game.Players.SelectMany(x => x.LostPieces).ToList();
            gameState.CapturedPieces = capturedPieces.Any()
                ? string.Join(string.Empty, capturedPieces.Select(x => x.ToFeNotation()))
                : null;

            game.ActiveTurn.GameState = gameState;
        }

        private string SolveCastlingRight(Game game)
        {
            var kings = game.ChessBoard.Pieces
                .Where(x => x.Type == PieceType.King)
                .OrderBy(x => x.Color)
                .Select(x => x as King);
            if (kings.Count() != 2 || kings.First().Color == kings.Last().Color)
                throw new InvalidOperationException("Both kings are required");

            var activeKing = kings.First(x => x.Color == game.ActivePlayer.Color);

            if (game.ActiveTurn.Move.Castle is not null)
            {
                activeKing.CastlingRights.Clear();
            }
            else if (game.ActiveTurn.Move.Piece.Type == PieceType.Rook)
            {
                activeKing.CastlingRights = activeKing.CastlingRights
                    .Where(x => game.ActiveTurn.Move.Piece.Square != x.Rook.Square)
                    .ToList();
            }

            return kings.SelectMany(x => x.CastlingRights).ToFeNotation();
        }
    }
}
