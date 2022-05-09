using HubooTechnicalExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HTETests.CarTests
{
    public class GivenGetNumberOfFailedMotTests
    {
        [Fact]
        public void WhenACarHasFailedMots_ShouldReturnTheNumberOfFailedTests()
        {
            //Arrange
            var expected = 2;
            var car = new Car()
            {
                MotTests = new List<MotTest>
                {
                    new MotTest() { testResult = "PASSED"},
                    new MotTest() { testResult = "FAILED" },
                    new MotTest() { testResult = "PASSED"},
                    new MotTest() { testResult = "FAILED" }
                }
            };
            //Act
            var actual = car.GetNumberOfFailedMotTests();
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenACarHasNoFailedMots_ShouldReturnNoFailedTests()
        {
            //Arrange
            var expected = 0;
            var car = new Car()
            {
                MotTests = new List<MotTest>
                {
                    new MotTest() { testResult = "PASSED"},
                    new MotTest() { testResult = "PASSED"},
                }
            };
            //Act
            var actual = car.GetNumberOfFailedMotTests();
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenACarHasNoMots_ShouldReturnNoFailedTests()
        {
            //Arrange
            var expected = 0;
            var car = new Car()
            {
                MotTests = new List<MotTest>{ }
            };
            //Act
            var actual = car.GetNumberOfFailedMotTests();
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenACarHasALowerCaseMotResults_ShouldNotImpactResult()
        {
            //Arrange
            var expected = 1;
            var car = new Car()
            {
                MotTests = new List<MotTest>
                {
                    new MotTest() { testResult = "passed"},
                    new MotTest() { testResult = "passed"},
                    new MotTest() { testResult = "failed"},
                }
            };
            //Act
            var actual = car.GetNumberOfFailedMotTests();
            //Assert
            Assert.Equal(expected, actual);
        }

    }
} 