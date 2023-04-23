using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace Json
{
    public static class JsonNumber
    {
        const string Exponent = "e";
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

            return IsInteger(GetInteger(input, dotIndex, exponentIndex)) && IsFraction(GetFraction(input, dotIndex, exponentIndex)) && IsExponent(GetExponent(input, exponentIndex));
        }

        public static bool HasValidDigits(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] < '0' || input[i] > '9') && input[i] != 'e')
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetInteger(string input, int dotIndex, int exponentIndex)
        {
            string fraction = GetFraction(input, dotIndex, exponentIndex);

            if (fraction.Length == 0 && input.Contains('.'))
            {
                return "Wrong";
            }

            if (input.Length > 1 && input.StartsWith('0') && !input.Contains('.'))
            {
                return "Wrong";
            }

            if (dotIndex != -1)
            {
                return input.Substring(0, dotIndex);
            }

            if (exponentIndex != -1)
            {
                return input.Substring(0, exponentIndex);
            }

            return input;
        }

        public static bool IsInteger(string input)
        {
            int i = 0;

            if (input[i] == '-')
            {
                i++;
            }

            if (input[i] == '0' && input.Length - i > 1)
            {
                return false;
            }

            return HasValidDigits(input.Substring(i)) && !input.Substring(i).Contains('e');
        }

        public static string GetFraction(string input, int dotIndex, int exponentIndex)
        {
            if (dotIndex != -1)
            {
                return input.Substring(dotIndex + 1);
            }

            return string.Empty;
        }

        public static bool IsFraction(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            if (input.Contains("."))
            {
                return false;
            }

            if (!input.Contains('e'))
            {
                return HasValidDigits(input);
            }

            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] < '0' || input[i] > '9') && !Operators.Contains(input[i]) && !Exponent.Contains(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetExponent(string input, int exponentIndex)
        {
            if (exponentIndex != -1)
            {
                if (input[exponentIndex - 1] < '0' || input[exponentIndex - 1] > '9')
                {
                    return "Wrong";
                }

                return input.Substring(exponentIndex);
            }

            return string.Empty;
        }

        public static bool IsExponent(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            if (Exponent.Contains(input[input.Length - 1]) || Operators.Contains(input[input.Length - 1]))
            {
                return false;
            }

            int operators = 0;

            for (int i = 1; i < input.Length; i++)
            {
                if ((input[i] < '0' || input[i] > '9') && !Operators.Contains(input[i]))
                {
                    return false;
                }

                if (Operators.Contains(input[i]))
                {
                    operators++;
                }
            }

            return operators <= 1;
        }
    }
}
