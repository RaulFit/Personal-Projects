using System;
using System.Linq;

namespace Json
{
    public static class JsonNumberValidator
    {
        const string Operators = "+-";

        public static bool IsJsonNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = input.ToLower();
            int dotIndex = input.IndexOf('.');
            int exponentIndex = input.IndexOf('e');

            return IsInteger(GetInteger(input, dotIndex, exponentIndex))
                && IsFraction(GetFraction(input, dotIndex, exponentIndex))
                && IsExponent(GetExponent(input, exponentIndex));
        }

        public static bool HasValidDigits(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    return false;
                }
            }

            return input.Length > 0;
        }

        public static string GetInteger(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex != -1)
            {
                return input[..dotIndex];
            }

            if (exponentIndex != -1)
            {
                return input[..exponentIndex];
            }

            return input;
        }

        public static bool IsInteger(string input)
        {
            if (input[0] == '-')
            {
                input = input[1..];
            }

            if (input.StartsWith('0') && input.Length > 1)
            {
                return false;
            }

            return HasValidDigits(input);
        }

        public static string GetFraction(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex != -1 && exponentIndex != -1)
            {
                return input[dotIndex..exponentIndex];
            }

            if (dotIndex != -1)
            {
                return input[dotIndex..];
            }

            return string.Empty;
        }

        public static bool IsFraction(string input)
        {
            return input == string.Empty || HasValidDigits(input[1..]);
        }

        public static string GetExponent(string input, int exponentIndex)
        {
            if (exponentIndex != -1)
            {
                return input[exponentIndex..];
            }

            return string.Empty;
        }

        public static bool IsExponent(string input)
        {
            if (input == string.Empty)
            {
                return true;
            }

            if (input.Length > 1)
            {
                input = input[1..];

                if (Operators.Contains(input[0]))
                {
                    input = input[1..];
                }
            }

            return HasValidDigits(input);
        }
    }
}
