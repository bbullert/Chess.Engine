namespace Chess.Engine.Extensions
{
    public static partial class IntExtensions
    {
        public static string ToFileName(this int fileIndex)
        {
            return ((char)(96 + fileIndex)).ToString();
        }
    }
}
