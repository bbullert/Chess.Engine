using Chess.Engine.Enums;
using Chess.Engine.Extensions;

namespace Chess.Engine.Components
{
    public class ChessBoard
    {
        public ChessBoard(int fileCount = 8, int rankCount = 8)
        {
            if (fileCount != 8 || rankCount != 8)
                throw new NotImplementedException("Unsupported chessboard dimensions");

            FileCount = fileCount;
            RankCount = rankCount;
            
            Squares = new List<Square>();
            for (int i = 1; i <= RankCount; i++)
            {
                var color = i % 2 == 0 ? Color.White : Color.Black;

                for (int j = 1; j <= FileCount; j++)
                {
                    var square = new Square(j, i)
                    {
                        ChessBoard = this,
                        Color = color
                    };
                    Squares.Add(square);
                    color = color == Color.White ? Color.Black : Color.White;
                }
            }

            FileLabels = new List<string>();
            for (int i = 0; i < FileCount; i++)
            {
                var square = Squares.ElementAt(i);
                FileLabels.Add(square.FileName);
            }

            RankLabels = new List<string>();
            for (int i = 0; i < RankCount; i++)
            {
                var square = Squares.ElementAt(i * RankCount);
                RankLabels.Add(square.RankName);
            }
        }

        public int FileCount { get; protected set; }
        public int RankCount { get; protected set; }
        public int SquareCount => FileCount * RankCount;
        public IList<string> FileLabels { get; protected set; }
        public IList<string> RankLabels { get; protected set; }
        public IList<Square> Squares { get; protected set; }
        public IEnumerable<IPiece> Pieces => Squares
            .Where(x => x.Piece is not null)
            .Select(x => x.Piece).ToList();

        public void Initialize(Game game)
        {
            foreach (var piece in Pieces)
            {
                piece.Initialize(game);
            }
        }

        public Square FindSquare(int file, int rank)
        {
            return Squares.FirstOrDefault(x => x.File == file && x.Rank == rank);
        }

        public Square FindSquare(string squareName)
        {
            return Squares.FirstOrDefault(x => x.ToString() == squareName);
        }

        public void AddPiece(IPiece piece, string squareName)
        {
            var square = FindSquare(squareName);
            if (square is null)
                throw new ArgumentNullException();
            square.Assign(piece);
        }

        public override string ToString()
        {
            return this.ToFeNotation();
        }
    }
}
