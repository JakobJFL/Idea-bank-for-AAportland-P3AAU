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
        /*
        [Theory]
        // arrange
        [InlineData(1, "Ikke angivet")]
        [InlineData(2, "Lav")]
        [InlineData(3, "Mellem")]
        [InlineData(4, "Høj")]
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
            string result = DBConvert.GetStatusStr(input);

            // assert
            Assert.Equal(expected, result);
        }
        

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(6)]
        public void GetStatusStr_InvalidDomain(int input)
        {
            Assert.Throws<ArgumentException>(() => DBConvert.GetStatusStr(input));
        }
        */
    }
}