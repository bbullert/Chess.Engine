using Chess.Engine.Components;
using Chess.Engine.Exceptions;

namespace Chess.Engine.Extensions
{
    public static partial class StringExtensions
    {
        public static ChessBoard ToChessBoard(this string chessBoardString)
        {
            var chessBoardArray = SolveChessBoard(chessBoardString);
            var chessBoard = new ChessBoard(chessBoardArray[0].Length, chessBoardArray.Length);

            for (int i = 0; i < chessBoardArray.Length; i++)
            {
                for (int j = 0; j < chessBoardArray[i].Length; j++)
                {
                    var c = chessBoardArray[i][j];
                    if (char.IsLetter(c))
                    {
                        var piece = c.ToPiece();
                        var square = chessBoard.FindSquare(j + 1, i + 1);
                        chessBoard.AddPiece(piece, square.ToString());
                    }
                }
            }

            return chessBoard;
        }

        private static char[][] SolveChessBoard(string chessBoardString)
        {
            var ranks = new List<char[]>();

            var rankStrings = chessBoardString.Split('/').ToArray();
            if (rankStrings.Length == 0)
                throw new ArgumentException(ErrorHelper.InvalidFeNotationSyntax);
            for (int i = rankStrings.Length - 1; i >= 0; i--)
            {
                var rankString = rankStrings[i];
                var rank = SolveRank(rankString);
                if (rank.Length == 0)
                    throw new ArgumentException(ErrorHelper.InvalidFeNotationSyntax);
                var lastRank = ranks.LastOrDefault();
                if (lastRank is not null &&
                    lastRank.Length != rank.Length)
                    throw new ArgumentException(ErrorHelper.InvalidFeNotationSyntax);
                ranks.Add(rank);
            }

            return ranks.ToArray();
        }

        private static char[] SolveRank(string rankString)
        {
            var rank = new List<char>();
            string substr = null;

            for (int i = 0; i < rankString.Length; i++)
            {
                char c = rankString[i];
                if (char.IsLetter(c))
                {
                    if (substr is not null)
                    {
                        rank.AddRange(SolveGap(substr));
                        substr = null;
                    }
                    rank.Add(c);
                }
                else if (char.IsDigit(c))
                {
                    substr += c;
                }
                else
                {
                    throw new ArgumentException(ErrorHelper.InvalidFeNotationSyntax);
                }
            }
            if (substr is not null)
            {
                rank.AddRange(SolveGap(substr));
                substr = null;
            }

            return rank.ToArray();
        }

        private static char[] SolveGap(string substr)
        {
            int count;
            if (!int.TryParse(substr, out count))
                throw new ArgumentException(ErrorHelper.InvalidFeNotationSyntax);

            return new char[count];
        }
    }
}
