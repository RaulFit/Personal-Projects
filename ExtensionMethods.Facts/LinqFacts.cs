﻿using Stock;
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
            Assert.Equal("", Linq.GenerateSubarraysWithSumLessOrEqualTo(new int[]{ }, 6));
        }

        [Fact]
        public void GenerateSubarraysWithSumLessOrEqualToK_ShouldReturnAllPossibleArrays()
        {
            var result = """
                1 2 3 4
                12 23
                123
                """;
            Assert.Equal(result, Linq.GenerateSubarraysWithSumLessOrEqualTo(new int[] {1, 2, 3, 4}, 6));
        }

        [Fact]
        public void GenerateAllCombinationsEqualTo_ShouldReturnAllPossibleCombinationsEqualToK()
        {
            var result = """
                -1+2+3+4-5=3
                1-2+3-4+5=3
                -1-2-3+4+5=3
                """;
            Assert.Equal(result, Linq.GenerateAllCombinationsEqualTo(5, 3));
        }

        [Fact]
        public void GenerateAllCombinationsEqualTo_NoPossibleCombinations_ShouldReturnEmptyString()
        {
            Assert.Equal("", Linq.GenerateAllCombinationsEqualTo(0, 2));
        }

        [Fact]
        public void GenerateTriplets_ArrayLengthLessThanThree_ShouldReturnEmptyString()
        {
            Assert.Equal("", Linq.GenerateTriplets(new int[] {1, 2}));
        }

        [Fact]
        public void GenerateTriplets_ShouldReturnResult()
        {
            int[] nums = new int[] { 3, 4, 5, 12, 13, 8, 15, 17};
            var result = """
                3^2 + 4^2 = 5^2
                5^2 + 12^2 = 13^2
                8^2 + 15^2 = 17^2
                """;
            Assert.Equal(result, Linq.GenerateTriplets(nums));
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
    }
}
