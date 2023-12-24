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

        private static void IsNull(string param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
