using System;

namespace Json
{
    public static class JsonNumber
    {
        const string Digits = "0123456789Ee-+.";

        public static bool IsJsonNumber(string input)
        {
            return IsNumber(input) && CanStartWithZeroIfFraction(input);
        }

        public static bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (!Digits.Contains(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanStartWithZeroIfFraction(string input)
        {
            if (input[0] == '0' && input.Length > 1)
            {
                return input.Contains(".");
            }

            return true;
        }
    }
}
