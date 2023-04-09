using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Json
{
    public static class JsonString
    {
        const int FirstNonUnicodeChar = 32;
        const int HexaDigits = 4;
        const string EscapeChars = "\"\\/bfnrtu";
        const string Hexa = "0123456789ABCDEFabcdef";

        public static bool IsJsonString(string input)
        {
            return HasContent(input) && IsDoubleQuoted(input) && !ContainsControlCharacters(input) && !ContainsUnrecognizedEscapeCharacters(input) && !EndsWithReverseSolidus(input) && !ContainsUnfinishedHexNumber(input);
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

        public static bool ContainsUnrecognizedEscapeCharacters(string input)
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == '\\')
                {
                    if (i < input.Length - 1 && EscapeChars.Contains(input[i + 1]))
                    {
                        i++;
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool EndsWithReverseSolidus(string input)
        {
            return input[input.Length - 2] == '\\';
        }

        public static bool ContainsUnfinishedHexNumber(string input)
        {
            int lastPossibleUnicode = input.Length - 2;
            for (int i = 0; i < lastPossibleUnicode; ++i)
            {
                if (input[i] == '\\' && input[i + 1] == 'u')
                {
                    if (input.Length - 1 - i < HexaDigits)
                    {
                        return true;
                    }

                    i++;

                    for (int j = i + 1; j <= i + HexaDigits; j++)
                    {
                        if (!Hexa.Contains(input[j]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
