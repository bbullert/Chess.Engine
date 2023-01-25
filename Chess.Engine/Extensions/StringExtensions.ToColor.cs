using Chess.Engine.Enums;

namespace Chess.Engine.Extensions
{
    public static partial class StringExtensions
    {
        public static Color ToColor(this char value)
        {
            var color = value switch
            {
                'w' => Color.White,
                'b' => Color.Black,
                _ => throw new ArgumentException()
            };

            return color;
        }
    }
}
