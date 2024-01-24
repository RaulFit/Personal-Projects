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

        public static IEnumerable<string> GenerateAllCombinationsEqualTo(int n, int k)
        {
            IEnumerable<string> signs = new string[] { "" };

            return Enumerable.Range(1, n).Aggregate(signs, (signs, i) =>
                         signs.SelectMany(item => new string[]  { item + "+", item + "-" }))
                         .Where(combination => Enumerable.Range(0, n).Aggregate(0, (sum, i) => combination[i] == '-' ? sum - (i + 1) : sum + i + 1) == k)
                         .Select(combination => string.Join("", combination.Zip(Enumerable.Range(1, n), (sign, num) => $"{sign}{num}")) + $"={k}");
        }

        public static IEnumerable<string> GenerateTriplets(int[] nums)
        {
            int length = nums.Length;

            return Enumerable.Range(0, length - 2)
            .SelectMany(a => Enumerable.Range(a + 1, length - a - 1)
            .SelectMany(b => Enumerable.Range(b + 1, length - b - 1), (b, c) => (nums[a], nums[b], nums[c])))
            .Where(triplet => triplet.Item1 < triplet.Item2 && triplet.Item2 < triplet.Item3 &&
            triplet.Item1 * triplet.Item1 + triplet.Item2 * triplet.Item2 == triplet.Item3 * triplet.Item3)
            .Select(triplet => $"{triplet.Item1}^2 + {triplet.Item2}^2 = {triplet.Item3}^2");
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
            return products.Where(prod => prod.Features.Intersect(features).Any()).ToList();
        }

        public static List<Product> GetProductsWithAllFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(prod => !features.Except(prod.Features).Any()).ToList();
        }

        public static List<Product> GetProductsWithNoFeatures(List<Product> products, List<Feature> features)
        {
            return products.Where(prod => !prod.Features.Intersect(features).Any()).ToList();
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

        public static IEnumerable<(string, int)> GetMostUsedWords(string text)
        {
            IsNull(text);

            char[] splitChars = new char[] { ' ', '.', ',', '?', '!', ':', ';' };
            return text.Split(splitChars).Where(word => !string.IsNullOrEmpty(word)).GroupBy(word => word).Select(word => (word.Key, word.Count())).OrderByDescending(word => word.Item2);
        }

        public static bool IsValidSudoku(int[][] sudoku)
        {
            bool validRows = sudoku.All(row => row.Distinct().Count() == 9);

            bool validColumns = Enumerable.Range(0, 9).All(i => Enumerable.Range(0, 9).Select(j => sudoku[j][i]).ToList().Distinct().Count() == 9);

            bool validSquares = Enumerable.Range(0, 3).SelectMany(i => Enumerable.Range(0, 3)
                                .Select(j => sudoku.Skip(i * 3).Take(3)
                                .Select(row => row.Skip(j * 3).Take(3))))
                                .Select(group => group.SelectMany(n => n))
                                .All(group => group.ToList().Distinct().Count() == 9);

            return validRows && validColumns && validSquares;
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