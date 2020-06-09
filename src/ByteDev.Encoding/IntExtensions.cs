namespace ByteDev.Encoding
{
    internal static class IntExtensions
    {
        public static bool IsMultipleOf(this int source, int value)
        {
            return source % value == 0;
        }
    }
}