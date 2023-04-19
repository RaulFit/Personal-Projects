using System;
using System.Linq;

namespace Json
{
    public static class JsonNumber
    {
        const string DigitsAndOperators = "0123456789Ee-+.";
        const string UnfinishedExponent = "eE-+";

        public static bool IsJsonNumber(string input)
        {
            return IsNumber(input) && CanStartWithZeroIfFraction(input) && !EndsWithDot(input) && !HasMultipleFractionsOrExponets(input) && ExponentIsComplete(input) && ExponentIsAfterFraction(input);
        }

        public static bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (!DigitsAndOperators.Contains(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanStartWithZeroIfFraction(string input)
        {
            return !(input[0] == '0' && input.Length > 1) || input.Contains(".");
        }

        public static bool EndsWithDot(string input)
        {
            return input.EndsWith(".");
        }

        public static bool HasMultipleFractionsOrExponets(string input)
        {
            if (!input.Contains(".") && !input.Contains("e") && !input.Contains("E"))
            {
                return false;
            }

            int expCount = 0;
            int dotCount = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == '.')
                {
                    dotCount++;
                }

                if (input[i] == 'e' || input[i] == 'E')
                {
                    expCount++;
                }
            }

            return dotCount > 1 || expCount > 1;
        }

        public static bool ExponentIsComplete(string input)
        {
            return !UnfinishedExponent.Contains(input[input.Length - 1]);
        }

        public static bool ExponentIsAfterFraction(string input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == 'e' || input[i] == 'E')
                {
                    for (int j = i + 1; j < input.Length; j++)
                    {
                        if (input[j] == '.')
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
