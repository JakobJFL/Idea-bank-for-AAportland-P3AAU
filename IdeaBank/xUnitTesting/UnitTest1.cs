using BusinessLogicLib;
using Xunit;

namespace XUnitTesting

{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Test \r\n Test \n Test", "Test <br /> Test <br /> Test")]
        [InlineData("Test \r\n\n Test \n\n", "Test <br /><br /> Test <br /><br />")]
        public void StrNewLineToBr_StringWNL_StringWBr(string input, string expected)
        {
            // act
            string result = DBConvert.StrNewLineToBr(input);

            // assert
            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("Test <br /> Test <br /> Test", "Test \n Test \n Test")]
        [InlineData("Test <br /><br /> Test <br />", "Test \n\n Test \n")]
        public void StrBrToNewLine_StringWBr_StringWNL(string input, string expected)
        {
            // act
            string result = DBConvert.StrBrToNewLine(input);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
