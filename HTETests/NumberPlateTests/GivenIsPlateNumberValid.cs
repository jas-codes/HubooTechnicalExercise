using HubooTechnicalExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HTETests.NumberPlateTests
{
    public class GivenIsPlateNumberValid
    {
        [Fact]
        public void WhenPlateNumberLengthIsGreaterThanSeven_ReturnFalse()
        {
            //Assign
            var numberPlate = new NumberPlate();
            numberPlate.Registration = "12345678";
            //Act
            var actual = numberPlate.IsPlateRegistrationValid;
            //Actual
            Assert.False(actual);
        }
        
        [Fact]
        public void WhenPlateRegistrationIsNull_ReturnFalse()
        {
            //Assign
            var numberPlate = new NumberPlate();
            numberPlate.Registration = null;
            //Act
            var actual = numberPlate.IsPlateRegistrationValid;
            //Actual
            Assert.False(actual);
        }

        [Theory]
        [InlineData("123456!")]
        [InlineData("12345!!")]
        [InlineData("!2!45!!")]
        [InlineData("!2-5!!")]
        [InlineData("*23557")]
        [InlineData("123+57")]
        [InlineData("123=57")]
        [InlineData("123$57")]
        [InlineData("123\"57")]
        [InlineData("")]
        public void WhenPlateNumberIncludesSymbolOrWhiteSpace_ReturnFalse(string input)
        {
            //Assign
            var numberPlate = new NumberPlate();
            numberPlate.Registration = input;
            //Act
            var actual = numberPlate.IsPlateRegistrationValid;
            //Actual
            Assert.False(actual);
        }

        [Theory]
        [InlineData(" 1234567")]
        [InlineData("1234567")]
        [InlineData("1234567 ")]
        [InlineData("1234 567")] 
        [InlineData("1234")]
        public void WhenPlateNumberLengthIsNotGreaterThanSeven_ReturnTrue(string input)
        {
            //Assign
            var numberPlate = new NumberPlate();
            numberPlate.Registration = input;
            //Act
            var actual = numberPlate.IsPlateRegistrationValid;
            //Actual
            Assert.True(actual);
        }
    }
}
