using HubooTechnicalExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HTETests.NumberPlateTests
{
    public class GivenRegistration
    {
        [Fact]
        public void WhenInitialisingRegistrationPropertyWithWhitespace_ShouldRemoveWhitespace()
        {
            //Assign
            var expected = "1234";
            var numberPlate = new NumberPlate();
            //Act
            numberPlate.Registration = " 12 3 4 ";
            //Assert
            Assert.True(numberPlate.Registration.Equals(expected));
        }
    }
}
