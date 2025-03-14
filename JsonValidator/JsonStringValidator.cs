using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Json
{
    public static class JsonStringValidator
    {
        const int LastControlCharacter = 31;
        const int Two = 2;
        const int HexaDigits = 4;
        const string EscapeChars = "\"\\/bfnrtu";
        const string Hexa = "0123456789ABCDEFabcdef";

        public static bool IsJsonString(string input)
        {
            return HasContent(input) && !EndsWithReverseSolidus(input) && IsDoubleQuoted(input) && !ContainsUnallowedCharacters(input);
        }

        public static bool IsDoubleQuoted(string input)
        {
            return input.StartsWith("\"") && input.EndsWith("\"");
        }

        public static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool ContainsControlCharsOrIncorrectHexNum(string input)
        {
           for (int i = 0; i < input.Length; i++)
            {
                if (input[i] <= LastControlCharacter)
                {
                    return true;
                }

                if (input[i] == '\\' && input[i + 1] == 'u' && !IsCorrectHexNumber(input, i + 1))
                {
                    return true;
                }
            }

           return false;
        }

        public static bool EndsWithReverseSolidus(string input)
        {
            return input.EndsWith("\\\"") && !input.EndsWith("\\\\\"");
        }

        public static bool ContainsUnrecognizedEscapeCharacters(string input)
        {
            int lastPossibleEscapeChar = input.Length - Two;
            int i = 0;
            while (i < lastPossibleEscapeChar)
            {
                if (input[i] == '\\')
                {
                    if (EscapeChars.Contains(input[i + 1]))
                    {
                        i += Two;
                        continue;
                    }

                    return true;
                }

                i++;
            }

            return false;
        }

        public static bool IsCorrectHexNumber(string input, int i)
        {
            if (input[input.Length - Two] == '\\' && input[input.Length - 1] == 'u')
            {
                return false;
            }

            if (input.Length - 1 - i < HexaDigits)
            {
                return false;
            }

            for (int j = i + 1; j <= i + HexaDigits; ++j)
            {
                if (!Hexa.Contains(input[j]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsUnallowedCharacters(string input)
        {
            return ContainsControlCharsOrIncorrectHexNum(input) || ContainsUnrecognizedEscapeCharacters(input);
        }
    }
}
