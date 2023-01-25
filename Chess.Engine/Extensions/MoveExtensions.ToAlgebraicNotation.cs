using Chess.Engine.Components;
using Chess.Engine.Enums;
using Chess.Engine.Factories;

namespace Chess.Engine.Extensions
{
    public static partial class MoveExtensions
    {
        public static string ToAlgebraicNotation(this Move move, Game game)
        {
            if (move.Piece is null)
                throw new ArgumentNullException();

            if (move.Type == MoveType.Castle)
            {
                if (move.Castle.CastleSide == CastleSide.KingSide)
                    return "0-0";
                if (move.Castle.CastleSide == CastleSide.QueenSide)
                    return "0-0-0";
            }

            var pieceFactory = new PieceFactory();

            string pieceStrongName = move.Piece
                .ToAlgebraicNotation()
                .ToString()
                .Replace("\0", string.Empty);

            string departureSquare = GetDepartureSquareDescription(move);

            string capture = move.Capture is not null 
                ? "x" : string.Empty;

            string arrivalSquare = move.ArrivalSquare.ToString();

            string enPassante = move.Type == MoveType.EnPassant 
                ? "ep" : string.Empty;

            string promotion = string.Empty;
            if (move.Promotion is not null)
            {
                pieceStrongName = string.Empty;
                var promotionPiece = pieceFactory.Create(move.Promotion.Value, move.Piece.Color);
                promotion = $"({promotionPiece.ToAlgebraicNotation()})";
            }

            string check = GetCheckDescription(move, game);

            return pieceStrongName +
                departureSquare +
                capture +
                arrivalSquare +
                enPassante +
                promotion +
                check;
        }

        private static string GetDepartureSquareDescription(Move move)
        {
            var claims = move.ArrivalSquare.Claims.Where(x => 
                x.DepartureSquare.HasType(move.Piece.Type) &&
                x.DepartureSquare != move.DepartureSquare);

            bool duplicateInFile = claims.Any(x => 
                x.DepartureSquare.File == move.DepartureSquare.File);

            bool duplicateInRank = claims.Any(x =>
                x.DepartureSquare.Rank == move.DepartureSquare.Rank);

            string departureSquare = null;
            if ((move.Piece.Type == PieceType.Pawn && 
                move.Capture is not null) || 
                duplicateInRank || 
                claims.Any()) 
                departureSquare += move.DepartureSquare.FileName;

            if (duplicateInFile)
                departureSquare += move.DepartureSquare.RankName;

            return departureSquare;
        }

        private static string GetCheckDescription(Move move, Game game)
        {
            var enemyPlayer = game.Players.FirstOrDefault(x =>
                x.Color == move.Piece.Color);
            if (enemyPlayer is null)
                throw new ArgumentNullException();

            var enemyKing = enemyPlayer.Pieces.FirstOrDefault(x =>
                x.Type == PieceType.King) as King;
            if (enemyKing is null)
                throw new ArgumentNullException();

            string value = enemyKing.IsInCheck ?
                "#" : new string('+', enemyKing.Checks.Count());

            return value;
        }
    }
}
