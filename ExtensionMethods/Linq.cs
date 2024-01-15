﻿using System.Net;
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
            int consonantsCount = word.Length - vowelsCount;

            return (vowelsCount, consonantsCount);
        }

        public static char FirstUniqueChar(string word)
        {
            IsNull(word);

            var groupWithCountOne = word.GroupBy(c => c).FirstOrDefault(group => group.Count() == 1);
            return groupWithCountOne != default ? groupWithCountOne.Key : '\0';
        }

        public static char MostCommonChar(string word)
        {
            IsNull(word);

            var groupWithCountOne = word.Length > 0 ? word.GroupBy(ch => ch).Aggregate((a, b) => a.Count() >= b.Count() ? a : b) : default;
            return groupWithCountOne != default ? groupWithCountOne.Key : '\0';
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

            return string.Join("\r\n", palindromes);
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

            return string.Join("\r\n", subArrays);
        }

        public static string GenerateAllCombinationsEqualTo(int n, int k)
        {
            var results = Enumerable.Range(0, 1 << n)
            .Select(bits =>
            {
                var permutation = Enumerable.Range(0, n)
                    .Select(n => (bits & (1 << n)) != 0 ? (n + 1) : -(n + 1))
                    .ToList();

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
