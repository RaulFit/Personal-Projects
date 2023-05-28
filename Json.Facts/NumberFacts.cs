using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class NumberFacts
    {
        [Theory]
        [InlineData("0", true)]
        [InlineData("a", false)]
        [InlineData("7", true)]
        [InlineData("70", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("-26", true)]
        [InlineData("-0", true)]
        [InlineData("12.34", true)]
        [InlineData("0.00000001", true)]
        [InlineData("10.00000001", true)]
        public void AllNumberFacts(string number, bool isNumber)
        {
            var num = new Number();
            Assert.Equal(isNumber, num.Match(number).Success());
        }
    }
}
