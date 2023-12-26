using System;
using System.Collections.Generic;
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
            return word.GroupBy(e => e).OrderByDescending(group => group.Count()).Select(group => group.Key).FirstOrDefault();
        }

        public static string GenerateAllPalindromes(string word)
        {
            IsNull(word);

            if(word.Length == 0)
            {
                return word;
            }

            var palindromes = Enumerable.Range(0, 2 * word.Length - 1).Select(
                center =>
                {
                    int left = center / 2;
                    int right = left + center % 2;
                    return GetAllPalindromes(word, left, right);
                }).SelectMany(palindrome => palindrome).ToList();

            var lengthOne = string.Join(" ", palindromes.Where(p => p.Length == 1));
            var lengthTwo = string.Join(" ", palindromes.Where(p => p.Length == 2));
            var others = string.Join("\r\n", palindromes.Where(p => p.Length > 2));

            return $"{lengthOne}\r\n{lengthTwo}\r\n{others}";
        }

        private static IEnumerable<string> GetAllPalindromes(string word, int left, int right)
        {
            while (left >= 0 && right < word.Length && word[left] == word[right])
            {
                string palindrome = word.Substring(left, right - left + 1);
                yield return palindrome;
                left--;
                right++;
            }
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
