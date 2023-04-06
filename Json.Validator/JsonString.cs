using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return HasContent(input) && IsDoubleQuoted(input);
        }

        public static bool IsDoubleQuoted(string input)
        {
            return input.StartsWith("\"") && input.EndsWith("\"");
        }

        public static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
