using Chess.Engine.Components;
using Chess.Engine.Exceptions;

namespace Chess.Engine.Extensions
{
    public static partial class StringExtensions
    {
        public static Square ToChessBoardSquare(this string value)
        {
            var chars = value.ToCharArray().ToList();
            if (chars.Count() < 2)
                throw new ArgumentException(ErrorHelper.InvalidSquareNameSyntax);

            var letter = chars.First();
            if (!char.IsLetter(letter))
                throw new ArgumentException(ErrorHelper.InvalidSquareNameSyntax);
            int file = (int)(letter - 96);

            var substr = string.Join(string.Empty, chars.Skip(1));
            int rank;
            if (!int.TryParse(substr, out rank))
                throw new ArgumentException(ErrorHelper.InvalidSquareNameSyntax);

            return new Square(file, rank);
        }
    }
}
