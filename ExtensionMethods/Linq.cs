using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Linq
{
    public class Linq
    {
        public static (int vowels, int consonants) CountVowelsAndConsonants(string word)
        {
            IsNull(word);

            string vowels = "aeiouAEIOU";
            int vowelsCount = word.Count(vowels.Contains);
            int consonantsCount = word.Count(e => !vowels.Contains(e));
            return (vowelsCount, consonantsCount);
        }

        public static char FirstUniqueChar(string word)
        {
            IsNull(word);

            var uniqueChars = word.GroupBy(c => c).Where(group => group.Count() == 1).Select(group => group.Key);
            return uniqueChars.FirstOrDefault();
        }

        public static char MostCommonChar(string word)
        {
            IsNull(word);
            return word.GroupBy(e => e).OrderByDescending(group => group.Count()).SelectMany(e => e).FirstOrDefault();
        }

        public static string GenerateAllPalindromes(string word)
        {
            IsNull(word);

            if (word.Length == 0)
            {
                return word;
            }

            var palindromes = Enumerable.Range(0, word.Length)
                .SelectMany(length => Enumerable.Range(0, word.Length - length + 1),
                word.Substring).Where(word => word.SequenceEqual(word.Reverse()));

            var lengthOne = string.Join(" ", palindromes.Where(p => p.Length == 1));
            var lengthTwo = string.Join(" ", palindromes.Where(p => p.Length == 2));
            var others = string.Join("\r\n", palindromes.Where(p => p.Length > 2));

            return $"{lengthOne}\r\n{lengthTwo}\r\n{others}";
        }

        public static string GenerateSubarraysWithSumLessOrEqualTo(int[] nums, int k)
        {
            if (nums.Length == 0)
            {
                return "";
            }

            var subArrays = Enumerable.Range(0, nums.Length)
                .SelectMany(start => Enumerable.Range(0, nums.Length - start + 1),
                (start, length) => nums.Skip(start).Take(length)).Where(subArr => subArr.Sum() <= k)
                .Select(subArr => string.Join("", subArr));

            var lengthOne = string.Join(" ", subArrays.Where(p => p.Length == 1));
            var lengthTwo = string.Join(" ", subArrays.Where(p => p.Length == 2));
            var others = string.Join("\r\n", subArrays.Where(p => p.Length > 2));

            return $"{lengthOne}\r\n{lengthTwo}\r\n{others}";
        }

        public static string GenerateAllCombinationsEqualTo(int n, int k)
        {
            var permsNum = (int)Math.Pow(2, n);
            var results = Enumerable.Range(0, permsNum)
            .Select(bits =>
            {
                var permutation = Enumerable.Range(0, n)
                .Select(n => (bits & ((int)Math.Pow(2, n))) != 0 ? (n + 1) : -(n + 1)).ToList();

                var sum = permutation.Sum();
                var str = string.Join("+", permutation);

                return new { sum, str };
            })
            .Where(intermediate => intermediate.sum == k)
            .Select(intermediate => $"{intermediate.str}={k}".Replace("+-", "-"));

            return string.Join("\r\n", results);
        }

        public static string GenerateTriplets(int[] nums)
        {
            IEnumerable<string> result = from a in nums
                                         from b in nums
                                         from c in nums
                                         where a < b && b < c && a*a + b*b == c*c
                                         select $"{a}^2 + {b}^2 = {c}^2";

            return string.Join("\r\n", result);
        }

        private static void IsNull(string param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
