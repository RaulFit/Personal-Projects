using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public static string GenerateSubarraysWithSumLessOrEqualToK(int[] nums, int k)
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

        private static void IsNull(string param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
