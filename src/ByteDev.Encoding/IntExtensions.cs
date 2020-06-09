namespace ByteDev.Encoding
{
    internal static class IntExtensions
    {
        public static bool IsMultipleOf(this int source, int value)
        {
            if (value == 0)
                return true;

            return source % value == 0;
        }
    }
}