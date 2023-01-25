using Chess.Engine.Components;

namespace Chess.Engine.Extensions
{
    public static partial class PlayerExtensions
    {
        public static string GetName(this Player player)
        {
            return player.Color.ToString().ToLower() + "s";
        }
    }
}
