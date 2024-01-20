﻿using System.Globalization;
using System.Runtime.CompilerServices;

namespace Linq
{
    public class Linq
    {
        public static (int vowels, int consonants) CountVowelsAndConsonants(string word)
        {
            IsNull(word);

            string vowels = "aeiouAEIOU";

            var letters = word.Where(char.IsLetter);
            int vowelsCount = letters.Count(vowels.Contains);
            int consonantsCount = letters.Count() - vowelsCount;

            return (vowelsCount, consonantsCount);
        }

        public static char FirstUniqueChar(string word)
        {
            IsNull(word);

            var groupWithCountOne = word.GroupBy(c => c).FirstOrDefault(group => group.Count() == 1);
            return groupWithCountOne != default ? groupWithCountOne.Key : '\0';
        }

        public static int ConvertToInt(string num)
        {
            IsNull(num);

            int result = num.Aggregate(0, (a, b) =>
            {
                if (!char.IsNumber(b) && num.IndexOf(b) != 0 && b != '-')
                {
                    throw new FormatException("The string was not in a correct format");
                }            

                return char.IsNumber(b) ? a * 10 + (b - '0') : 0;
            });

            return num[0] == '-' ? -result : result;
        }

        public static char MostCommonChar(string word)
        {
            IsNull(word);

            var res = word.GroupBy(ch => ch).MaxBy(group => group.Count());
            return res != null ? res.Key : '\0';
        }

        public static IEnumerable<string> GenerateAllPalindromes(string word)
        {
            IsNull(word);

            return Enumerable.Range(0, word.Length)
                .SelectMany(length => Enumerable.Range(0, word.Length - length + 1),
                word.Substring).Where(word => word.SequenceEqual(word.Reverse()) && word.Count() > 0);
        }

        public static IEnumerable<string> GenerateSubarraysWithSumLessOrEqualTo(int[] nums, int k)
        {
            return Enumerable.Range(0, nums.Length)
                .SelectMany(start => Enumerable.Range(0, nums.Length - start + 1),
                (start, length) => nums.Skip(start).Take(length)).Where(subArr => subArr.Sum() <= k && subArr.Count() > 0)
                .Select(subArr => string.Join("", subArr));
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

        public static IEnumerable<string> GenerateTriplets(int[] nums)
        {
            return nums
        .SelectMany(a => nums.Where(b => b > a), (a, b) => (a, b))
        .SelectMany(pair => nums.Where(c => c > pair.b), (pair, c) => (pair.a, pair.b, c))
        .Where(triplet => triplet.a * triplet.a + triplet.b * triplet.b == triplet.c * triplet.c)
        .Select(triplet => $"{triplet.a}^2 + {triplet.b}^2 = {triplet.c}^2");
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
