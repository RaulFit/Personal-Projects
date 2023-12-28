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
            string result = """
                a a b a a c
                aa aa
                aabaa
                aba
                """;
            Assert.Equal(result, Linq.GenerateAllPalindromes("aabaac"));
        }

        [Fact]
        public void GenerateAllPalindromes_ShouldWorkWhenStringIsEmpty()
        {
            Assert.Equal("", Linq.GenerateAllPalindromes(""));
        }

        [Fact]
        public void GenerateSubarraysWithSumLessOrEqualToK_EmptyArray_ShouldReturnEmptyString()
        {
            Assert.Equal("", Linq.GenerateSubarraysWithSumLessOrEqualToK(new int[]{ }, 6));
        }

        [Fact]
        public void GenerateSubarraysWithSumLessOrEqualToK_ShouldReturnAllPossibleArrays()
        {
            var result = """
                1 2 3 4
                12 23
                123
                """;
            Assert.Equal(result, Linq.GenerateSubarraysWithSumLessOrEqualToK(new int[] {1, 2, 3, 4}, 6));
        }
    }
}
