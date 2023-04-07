using System;

namespace Json
{
    public static class JsonString
    {
        const int FirstNonUnicodeChar = 32;

        public static bool IsJsonString(string input)
        {
            return HasContent(input) && IsDoubleQuoted(input) && !ContainsControlCharacters(input);
        }

        public static bool IsDoubleQuoted(string input)
        {
            return input.StartsWith("\"") && input.EndsWith("\"");
        }

        public static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool ContainsControlCharacters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < FirstNonUnicodeChar)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
