using HubooTechnicalExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HTETests.CarTests
{
    public class GivenGetNextMotDueDate
    {
        [Fact]
        public void WhenACarHasMots_ShouldReturnTheLatestExpiryDate()
        {
            //Arrange
            var expected = DateTime.Now.AddYears(1).Date;
            var car = new Car()
            {
                MotTests = new List<MotTest>
                {
                    new MotTest() { ExpiryDate = DateTime.Now},
                    new MotTest() { ExpiryDate = DateTime.Now.AddDays(1)},
                    new MotTest() { ExpiryDate = DateTime.Now.AddDays(-10)},
                    new MotTest() { ExpiryDate = DateTime.Now.AddYears(1)},
                    new MotTest() { ExpiryDate = DateTime.Now.AddDays(2)},
                }
            };
            //Act
            var actual = car.GetNextMotDueDate();
            //Assert
            Assert.Equal(expected, actual);
        }

        //A car should always have an MOT
        [Fact]
        public void WhenACarHasNoMots_ShowThrow()
        {
            //Arrange
            var car = new Car()
            {
                MotTests = new List<MotTest> { }
            };
            //Act, Assert
            Assert.Throws<InvalidOperationException>(() => car.GetNextMotDueDate());
        }

        //A car should always have an MOT
        [Fact]
        public void WhenACarHasNull_ShouldThrow()
        {
            //Arrange
            var car = new Car()
            {
                MotTests = null
            };
            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => car.GetNextMotDueDate());
        }
    }
}
