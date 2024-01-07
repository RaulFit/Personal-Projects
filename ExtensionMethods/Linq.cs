using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

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

        public class Product
        {
            public string Name { get; set; }
            public ICollection<Feature> Features { get; set; }
        }

        public class Feature
        {
            public int Id { get; set; }
        }

        public static List<Product> GetProductsWithFeature(List<Product> products, List<Feature> features)
        {
            return products.Where(prod => features.Any(feature => prod.Features.Contains(feature))).ToList();
        }

        public static List<Product> GetProductsWithAllFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(prod => features.All(feature => prod.Features.Contains(feature))).ToList();
        }

        public static List<Product> GetProductsWithNoFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(prod => features.All(feature => !prod.Features.Contains(feature))).ToList();
        }

        public struct Prod
        {
            public string Name;
            public int Quantity;

            public Prod(string name, int quantity)
            {
                Name = name;
                Quantity = quantity;
            }
            public override string ToString()
            {
                return $"{Name} - {Quantity}";
            }
        }

        public static List<Prod> GetTotalQuantity(List<Prod> first, List<Prod> second)
        {
            return first.Concat(second).GroupBy(prod => prod.Name, prod => prod.Quantity).Select(group => new Prod(group.Key, group.Sum())).ToList();
        }

        public class TestResults
        {
            public string Id { get; set; }
            public string FamilyId { get; set; }
            public int Score { get; set; }

            public TestResults(string id, string familyId, int score)
            {
                Id = id;
                FamilyId = familyId;
                Score = score;
            }
        }

        public static List<TestResults> KeepMaxScore(List<TestResults> results)
        {
            return results.GroupBy(family => family.FamilyId).Select(group => group.Aggregate((a, b) => a.Score > b.Score ? a : b)).ToList();
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
