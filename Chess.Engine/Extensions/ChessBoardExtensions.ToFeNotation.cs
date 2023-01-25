using Chess.Engine.Components;

namespace Chess.Engine.Extensions
{
    public static partial class ChessBoardExtensions
    {
        public static string ToFeNotation(this ChessBoard chessBoard)
        {
            string value = string.Empty;
            for (int rank = chessBoard.RankCount; rank >= 1; rank--)
            {
                int gap = 0;
                for (int file = 1; file <= chessBoard.FileCount; file++)
                {
                    var square = chessBoard.Squares.First(x => 
                        x.File == file && x.Rank == rank);

                    if (!square.IsEmpty)
                    {
                        if (gap > 0)
                        {
                            value += gap;
                            gap = 0;
                        }

                        value += square.Piece.ToFeNotation();
                    }
                    else gap++;
                }
                if (gap > 0) value += gap;
                if (rank > 1) value += '/';
            }

            return value;
        }
    }
}
