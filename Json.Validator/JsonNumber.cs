using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace Json
{
    public static class JsonNumber
    {
        const string Digits = "0123456789.";
        const string Operators = "+-";
        const string IncompleteExponent = "Ee+-";

        public static bool IsJsonNumber(string input)
        {
            return HasContent(input) && !StartWithZero(input) && !EndsWithDot(input) && NumIsInCorrectFormat(input);
        }

        public static bool HasContent(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool StartWithZero(string input)
        {
            return input[0] == '0' && input.Length > 1 && !input.Contains('.');
        }

        public static bool EndsWithDot(string input)
        {
            return input.EndsWith(".");
        }

        public static bool NumIsInCorrectFormat(string input)
        {
            return !ContainsWrongCharacters(input) && !HasMultipleExponentsOrFractions(input) && !ExponentIsIncomplete(input) && !ExponentIsBeforeFraction(input);
        }

        public static bool ContainsWrongCharacters(string input)
        {
            int i = 0;
            if (input[i] == '-')
            {
                i++;
            }

            while (i < input.Length)
            {
                if (!Digits.Contains(input[i]) && input[i] != 'e' && input[i] != 'E' && !Operators.Contains(input[i]))
                {
                    return true;
                }

                if (Operators.Contains(input[i]) && input[i - 1] != 'e' && input[i - 1] != 'E')
                {
                    return true;
                }

                i++;
            }

            return false;
        }

        public static bool HasMultipleExponentsOrFractions(string input)
        {
            int exponents = 0;
            int fractions = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'e' || input[i] == 'E')
                {
                    exponents++;
                }

                if (input[i] == '.')
                {
                    fractions++;
                }
            }

            return exponents > 1 || fractions > 1;
        }

        public static bool ExponentIsIncomplete(string input)
        {
            return IncompleteExponent.Contains(input[input.Length - 1]);
        }

        public static bool ExponentIsBeforeFraction(string input)
        {
            if (!input.Contains('.'))
            {
                return false;
            }

            int fractionStart = input.IndexOf('.');
            for (int i = 0; i < fractionStart; ++i)
            {
                if (input[i] == 'e' || input[i] == 'E')
                {
                    return true;
                }
            }

            return false;
        }
    }
}
