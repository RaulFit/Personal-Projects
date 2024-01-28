using Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Facts
{
    public class LinqFacts
    {
        [Fact]
        public void CountVowelsAndConsonants_StringIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Linq.CountVowelsAndConsonants(null));
        }

        [Fact]
        public void CountVowelsAndConsonants_ShouldWorkCorrectlyWhenStrinIsEmpty()
        {
            Assert.Equal((0, 0), Linq.CountVowelsAndConsonants(""));
        }

        [Fact]
        public void CountVowelsAndConsonants_StringIsEmpty_ShouldHaveZeroVowelsAndConsonants()
        {
            string word = "hello";
            Assert.Equal((2, 3), Linq.CountVowelsAndConsonants(word));
        }

        [Fact]
        public void CountVowelsAndConsonants_ShouldWorkCorrectlyWithUpperAndLowerCaseLetters()
        {
            string word = "HeLlO";
            Assert.Equal((2, 3), Linq.CountVowelsAndConsonants(word));
        }

        [Fact]
        public void FirstUniqueChar_ShoulThrowArgumentNullExceptionWhenStringIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Linq.FirstUniqueChar(null));
        }

        [Fact]
        public void FirstUniqueChar_EmptyString_ShouldReturnDefaultValue()
        {
            Assert.Equal('\0', Linq.FirstUniqueChar(""));
        }

        [Fact]
        public void FirstUniqueChar_ShouldReturnFirstUniqueCharInString()
        {
            Assert.Equal('i', Linq.FirstUniqueChar("airplane"));
        }

        [Fact]
        public void ConvertToInt_StringIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Linq.ConvertToInt(null));
        }

        [Fact]
        public void ConvertToInt_IncorrectFormat_ShouldThrowFormatException()
        {
            Assert.Throws<FormatException>(() => Linq.ConvertToInt("15-a"));
        }

        [Fact]
        public void ConvertToInt_NoMinus_ShouldConvertStringToPositiveNumber()
        {
            Assert.Equal(12, Linq.ConvertToInt("12"));
        }

        [Fact]
        public void ConvertToInt_WithMinus_ShouldConvertStringToNegativeNumber()
        {
            Assert.Equal(-12, Linq.ConvertToInt("-12"));
        }

        [Fact]
        public void MostCommonChar_EmptyString_ShouldReturnDefaultValue()
        {
            Assert.Equal('\0', Linq.MostCommonChar(""));
        }

        [Fact]
        public void MostCommonChar_ValidString_ShouldReturnMostCommonChar()
        {
            Assert.Equal('a', Linq.MostCommonChar("abracadabra"));
        }

        [Fact]
        public void MostCommonChar_ValidStringWithNoMaxApparitions_ShouldReturnFirstChar()
        {
            Assert.Equal('o', Linq.MostCommonChar("ocean"));
        }

        [Fact]
        public void GenerateAllPalindromes_ValidStringShouldReturnAllPossiblePalindromes()
        {
            var results = new List<string>() { "a", "aa", "aabaa", "a", "aba", "b", "a", "aa", "a", "c"};

            Assert.Equal(results, Linq.GenerateAllPalindromes("aabaac"));
        }

        [Fact]
        public void GenerateAllPalindromes_ShouldWorkWhenStringIsEmpty()
        {
            Assert.Equal(new List<string>(), Linq.GenerateAllPalindromes(""));
        }

        [Fact]
        public void GenerateSubarraysWithSumLessOrEqualToK_EmptyArray_ShouldReturnEmptyCollection()
        {
            Assert.Equal(new List<string>(), Linq.GenerateSubarraysWithSumLessOrEqualTo(new int[]{ }, 6));
        }

        [Fact]
        public void GenerateSubarraysWithSumLessOrEqualToK_ShouldReturnAllPossibleArrays()
        {
            var results = new List<string>() { "1", "12", "123", "2", "23", "3", "4" };
    
            Assert.Equal(results, Linq.GenerateSubarraysWithSumLessOrEqualTo(new int[] {1, 2, 3, 4}, 6));
        }

        [Fact]
        public void GenerateAllCombinationsEqualTo_ShouldReturnAllPossibleCombinationsEqualToK()
        {
            var result = new List<string>() { "+1-2+3-4+5=3", "-1+2+3+4-5=3", "-1-2-3+4+5=3" };

            Assert.Equal(result, Linq.GenerateAllCombinationsEqualTo(5, 3));
        }

        [Fact]
        public void GenerateAllCombinationsEqualTo_NoPossibleCombinations_ShouldReturnEmptyCollection()
        {
            Assert.Equal(new List<string>(), Linq.GenerateAllCombinationsEqualTo(0, 2));
        }

        [Fact]
        public void GenerateTriplets_ArrayLengthLessThanThree_ShouldReturnEmptyCollection()
        {
            Assert.Equal(new List<string>(), Linq.GenerateTriplets(new int[] {1, 2}));
        }

        [Fact]
        public void GenerateTriplets_ShouldReturnResult()
        {
            int[] nums = new int[] { 3, 4, 5, 12, 13, 8, 15, 17 };
            var results = new List<string>() { "3^2 + 4^2 = 5^2", "5^2 + 12^2 = 13^2", "8^2 + 15^2 = 17^2" };

            Assert.Equal(results, Linq.GenerateTriplets(nums));
        }

        [Fact]
        public void GetProductsWithFeature_NoProducts_ShouldReturnEmptyList()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>();

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>();

            Linq.Product prod3 = new Linq.Product();
            prod3.Name = "laptop";
            prod3.Features = new List<Linq.Feature>() { };

            List<Linq.Feature> features = new List<Linq.Feature>() { first };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2, prod3 };

            var result = new List<Linq.Product>();

            Assert.Equal(result, Linq.GetProductsWithFeature(products, features));
        }

        [Fact]
        public void GetProductsWithFeature_ShouldReturnExpectedResult()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Feature second = new Linq.Feature();
            second.Id = 2;

            Linq.Feature third = new Linq.Feature();
            third.Id = 3;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>() { first, second };

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>() { third };

            Linq.Product prod3 = new Linq.Product();
            prod3.Name = "laptop";
            prod3.Features = new List<Linq.Feature>() { };

            List<Linq.Feature> features = new List<Linq.Feature>() { first, second, third };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2, prod3 };

            var result = new List<Linq.Product>() { prod1, prod2 };

            Assert.Equal(result, Linq.GetProductsWithFeature(products, features));
        }

        [Fact]
        public void GetProductsWithAllFeatures_NoProducts_ShouldReturnEmptyList()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>();

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>();

            List<Linq.Feature> features = new List<Linq.Feature>() { first };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2 };

            var result = new List<Linq.Product>();

            Assert.Equal(result, Linq.GetProductsWithAllFeatures(products, features));
        }

        [Fact]
        public void GetProductsWithAllFeatures_ShouldReturnOnlyProductsWithAllFeatures()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Feature second = new Linq.Feature();
            second.Id = 2;

            Linq.Feature third = new Linq.Feature();
            third.Id = 3;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>() { first, second, third};

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>() { first, second };

            Linq.Product prod3 = new Linq.Product();
            prod3.Name = "laptop";
            prod3.Features = new List<Linq.Feature>() { third };

            Linq.Product prod4 = new Linq.Product();
            prod4.Name = "tablet";
            prod4.Features = new List<Linq.Feature>() { first, second, third };

            List<Linq.Feature> features = new List<Linq.Feature>() { first, second, third };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2, prod3, prod4 };

            var result = new List<Linq.Product>() { prod1, prod4 };

            Assert.Equal(result, Linq.GetProductsWithAllFeatures(products, features));
        }

        [Fact]
        public void GetProductsWithNoFeatures_NoProductsWithZeroFeatures_ShouldReturnEmptyList()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>() { first };

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>() { first };

            List<Linq.Feature> features = new List<Linq.Feature>() { first };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2 };

            var result = new List<Linq.Product>();

            Assert.Equal(result, Linq.GetProductsWithNoFeatures(products, features));
        }

        [Fact]
        public void GetProductsWithNoFeatures_ShouldReturnOnlyProductsWithNoFeatures()
        {
            Linq.Feature first = new Linq.Feature();
            first.Id = 1;

            Linq.Feature second = new Linq.Feature();
            second.Id = 2;

            Linq.Feature third = new Linq.Feature();
            third.Id = 3;

            Linq.Product prod1 = new Linq.Product();
            prod1.Name = "phone";
            prod1.Features = new List<Linq.Feature>() { first, second, third };

            Linq.Product prod2 = new Linq.Product();
            prod2.Name = "charger";
            prod2.Features = new List<Linq.Feature>();

            Linq.Product prod3 = new Linq.Product();
            prod3.Name = "laptop";
            prod3.Features = new List<Linq.Feature>() { third };

            Linq.Product prod4 = new Linq.Product();
            prod4.Name = "tablet";
            prod4.Features = new List<Linq.Feature>();

            List<Linq.Feature> features = new List<Linq.Feature>() { first, second, third };
            List<Linq.Product> products = new List<Linq.Product> { prod1, prod2, prod3, prod4 };

            var result = new List<Linq.Product>() { prod2, prod4 };

            Assert.Equal(result, Linq.GetProductsWithNoFeatures(products, features));
        }

        [Fact]
        public void GetTotalQuantity_NoProducts_ShouldReturnEmptyList()
        {
            List<Linq.Prod> first = new List<Linq.Prod>();
            List<Linq.Prod> second = new List<Linq.Prod>();
            

            Assert.Equal(new List<Linq.Prod>(), Linq.GetTotalQuantity(first, second));
        }

        [Fact]
        public void GetTotalQuantity_ShouldReturnExpectedResult()
        {
            List<Linq.Prod> first = new List<Linq.Prod>
            {
                new Linq.Prod("phone", 13),
                new Linq.Prod("laptop", 6),
                new Linq.Prod("charger", 15),
                new Linq.Prod("tablet", 9)
            };

            List<Linq.Prod> second = new List<Linq.Prod>
            {
                new Linq.Prod("phone", 5),
                new Linq.Prod("tv", 14),
                new Linq.Prod("cpu", 12),
                new Linq.Prod("tablet", 10)
            };

            var result = new List<Linq.Prod>()
            {
                new Linq.Prod("phone", 18),
                new Linq.Prod("laptop", 6),
                new Linq.Prod("charger", 15),
                new Linq.Prod("tablet", 19),
                new Linq.Prod("tv", 14),
                new Linq.Prod("cpu", 12),
            };

            Assert.Equal(result, Linq.GetTotalQuantity(first, second));
        }

        [Fact]
        public void KeepMaxScore_NoProducts_ShouldReturnEmptyList()
        {
            List<Linq.TestResults> results = new List<Linq.TestResults>();

            Assert.Equal(new List<Linq.TestResults>(), Linq.KeepMaxScore(results));
        }

        [Fact]
        public void KeepMaxScore_ShouldOnlyKeepMaxScoreForEachFamily()
        {
            List<Linq.TestResults> results = new List<Linq.TestResults>
            {
                new Linq.TestResults("Alex", "Pop", 73),
                new Linq.TestResults("Michael", "Pop", 90),
                new Linq.TestResults("David", "Blake", 60),
                new Linq.TestResults("Erik", "Jones", 80),
                new Linq.TestResults("Steve", "Blake", 76),
                new Linq.TestResults("George", "Blake", 98),
            };

            var final = new List<Linq.TestResults>
            {
                new Linq.TestResults("Michael", "Pop", 90),
                new Linq.TestResults("George", "Blake", 98),
                new Linq.TestResults("Erik", "Jones", 80),
            };

            Assert.Equivalent(final, Linq.KeepMaxScore(results));
        }

        [Fact]
        public void GetMostUsedWords_EmptyString_ShouldReturnEmptyCollection()
        {
            Assert.Equal(new List<(string, int)>(), Linq.GetMostUsedWords(""));
        }

        [Fact]
        public void GetMostUsedWords_ValidString_ShouldOrderWordsDescendinglyByCount()
        {
            string text = "apple, pear, pear apple banana pear apple apple banana; pineapple";

            var result = new List<(string, int)>() { ("apple", 4), ("pear", 3), ("banana", 2), ("pineapple", 1)};
            Assert.Equal(result, Linq.GetMostUsedWords(text));
        }

        [Fact]
        public void IsValidSudoku_InvalidSudoku_ShouldReturnFalse()
        {
            int[][] sudoku = new int[9][];
            sudoku[0] = new int[] { 9, 9, 2, 1, 5, 4, 3, 8, 6 };
            sudoku[1] = new int[] { 6, 4, 3, 8, 2, 7, 1, 5, 9 };
            sudoku[2] = new int[] { 8, 5, 1, 3, 9, 6, 7, 2, 4 };
            sudoku[3] = new int[] { 2, 6, 5, 9, 7, 3, 8, 4, 1 };
            sudoku[4] = new int[] { 4, 8, 9, 5, 6, 1, 2, 7, 3 };
            sudoku[5] = new int[] { 3, 1, 7, 4, 8, 2, 9, 6, 5 };
            sudoku[6] = new int[] { 1, 3, 6, 7, 4, 8, 5, 9, 2 };
            sudoku[7] = new int[] { 9, 7, 4, 2, 1, 5, 6, 3, 8 };
            sudoku[8] = new int[] { 5, 2, 8, 6, 3, 9, 4, 1, 7 };

            Assert.False(Linq.IsValidSudoku(sudoku));
        }

        [Fact]
        public void IsValidSudoku_ShouldReturnFalseWhenLineContainsLessThanNineElements()
        {
            int[][] sudoku = new int[9][];
            sudoku[0] = new int[] { 7, 9, 2, 1, 5, 4, 3 };
            sudoku[1] = new int[] { 6, 4, 3, 8, 2, 7, 1, 5, 9 };
            sudoku[2] = new int[] { 8, 5, 1, 3, 9, 6, 7, 2, 4 };
            sudoku[3] = new int[] { 2, 6, 5, 9, 7, 3, 8, 4, 1 };
            sudoku[4] = new int[] { 4, 8, 9, 5, 6, 1, 2, 7, 3 };
            sudoku[5] = new int[] { 3, 1, 7, 4, 8, 2, 9, 6, 5 };
            sudoku[6] = new int[] { 1, 3, 6, 7, 4, 8, 5, 9, 2 };
            sudoku[7] = new int[] { 9, 7, 4, 2, 1, 5, 6, 3, 8 };
            sudoku[8] = new int[] { 5, 2, 8, 6, 3, 9, 4, 1, 7 };

            Assert.False(Linq.IsValidSudoku(sudoku));
        }

        [Fact]
        public void IsValidSudoku_ValidSudoku_ShouldReturnTrue()
        {
            int[][] sudoku = new int[9][];
            sudoku[0] = new int[] { 7, 9, 2, 1, 5, 4, 3, 8, 6 };
            sudoku[1] = new int[] { 6, 4, 3, 8, 2, 7, 1, 5, 9 };
            sudoku[2] = new int[] { 8, 5, 1, 3, 9, 6, 7, 2, 4 };
            sudoku[3] = new int[] { 2, 6, 5, 9, 7, 3, 8, 4, 1 };
            sudoku[4] = new int[] { 4, 8, 9, 5, 6, 1, 2, 7, 3 };
            sudoku[5] = new int[] { 3, 1, 7, 4, 8, 2, 9, 6, 5 };
            sudoku[6] = new int[] { 1, 3, 6, 7, 4, 8, 5, 9, 2 };
            sudoku[7] = new int[] { 9, 7, 4, 2, 1, 5, 6, 3, 8 };
            sudoku[8] = new int[] { 5, 2, 8, 6, 3, 9, 4, 1, 7 };

            Assert.True(Linq.IsValidSudoku(sudoku));
        }
    }
}
