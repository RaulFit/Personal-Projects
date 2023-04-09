using System;

namespace Json
{
    public static class JsonNumber
    {
        public static bool IsJsonNumber(string input)
        {
            return IsNumber(input);
        }

        public static bool IsNumber(string input)
        {
            return int.TryParse(input, out int result);
        }
    }
}
