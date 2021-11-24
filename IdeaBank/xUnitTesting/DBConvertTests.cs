using System;
using BusinessLogicLib;
using Xunit;

namespace XUnitTesting

{
    public class DBConvertTests
    {
        [Theory]
        // arrange
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
        // arrange
        [InlineData("Test <br /> Test <br /> Test", "Test \n Test \n Test")]
        [InlineData("Test <br /><br /> Test <br />", "Test \n\n Test \n")]
        public void StrBrToNewLine_StringWBr_StringWNL(string input, string expected)
        {
            // act
            string result = DBConvert.StrBrToNewLine(input);

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // arrange
        [InlineData(1, "Lav")]
        [InlineData(2, "Mellem")]
        [InlineData(3, "Høj")]
        [InlineData(0, "Ikke angivet")]
        public void GetPriorityStr_validDomain(int input, string expected)
        {
            // act
            string result = DBConvert.GetPriorityStr(input); // change GetPriorityStr to public for testing purposes

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // arrange
        [InlineData(1, "Oprettet")]
        [InlineData(2, "Godkendt")]
        [InlineData(3, "Arkiveret")]
        [InlineData(4, "Afsluttet")]
        public void GetStatusStr_validDomain(int input, string expected)
        {
            // act
            string result = DBConvert.GetStatusStr(input); // change GetStatusStr to public for testing purposes

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetStatusStr_InvalidDomain()
        {
            Assert.Throws<ArgumentException>(() => DBConvert.GetStatusStr(6)); // change GetStatusStr to public for testing purposes
        }
    }
}