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
        [InlineData("12.", false)]
        [InlineData("12.34.56", false)]
        [InlineData("12.3x", false)]
        [InlineData("12e3", true)]
        [InlineData("12E3", true)]
        [InlineData("12e+3", true)]
        [InlineData("61e-9", true)]
        [InlineData("12.34E3", true)]
        [InlineData("22e3x3", false)]
        [InlineData("22e323e33", false)]
        [InlineData("22e+", false)]
        [InlineData("22e3.3", false)]
        [InlineData("22-4", false)]
        [InlineData("61e+-9", false)]
        [InlineData("12.4-2+21", false)]
        public void AllNumberFacts(string number, bool isNumber)
        {
            var num = new Number();
            Assert.Equal(isNumber, num.Match(number).Success());
        }
    }
}
