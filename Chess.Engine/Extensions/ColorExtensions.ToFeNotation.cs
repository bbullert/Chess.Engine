using Chess.Engine.Enums;

namespace Chess.Engine.Extensions
{
    public static partial class ColorExtensions
    {
        public static char ToFeNotation(this Color color)
        {
            return color == Color.White ? 'w' : 'b';
        }
    }
}
