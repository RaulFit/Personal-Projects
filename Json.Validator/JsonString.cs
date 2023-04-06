using System;

namespace Json
{
    public static class JsonString
    {
        public static bool IsJsonString(string input)
        {
            return IsWrappedInDoubleQuotes(input);
        }

        public static bool IsWrappedInDoubleQuotes(string input)
        {
            return input.StartsWith("\"") && input.EndsWith("\"");
        }
    }
}
