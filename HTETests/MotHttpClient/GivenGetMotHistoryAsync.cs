using HubooTechnicalExercise;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq.Protected;
using Xunit;
using System.Threading;
using Newtonsoft.Json;

namespace HTETests.MotHttpClientTests
{
    public class GivenGetMotHistoryAsync
    {
        [Fact]
        public async Task WhenRegNumberIsEmptyOrNull_ShouldReturnNullCarAndNotFoundStatusCode()
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage())
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            MotHttpClient motHttpClient = new MotHttpClient(httpClient);

            //Act
            (Car actualCarNullTest, HttpStatusCode responseCodeNullTest) = await motHttpClient.GetMotHistoryAsync(null);
            (Car actualCarEmptyTest, HttpStatusCode responseCodeEmptyTest) = await motHttpClient.GetMotHistoryAsync(string.Empty);

            //Assert
            Assert.Null(actualCarNullTest);
            Assert.Null(actualCarEmptyTest);
            Assert.Equal(HttpStatusCode.NotFound, responseCodeNullTest);
            Assert.Equal(HttpStatusCode.NotFound, responseCodeEmptyTest);
            mockMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task WhenASuccessfulResponse_ShouldReturnTheFirstCarDataAndStatusCode()
        {

            //Arrange
            var regNumber = "111111";
            var expected = "TestCar1";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(new List<Car>()
                        {
                            new Car() { Make = "TestCar1" },
                            new Car() { Make = "TestCar2" }
                        }))
                })
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            MotHttpClient motHttpClient = new MotHttpClient(httpClient);

            //Act
            (Car actualCar, HttpStatusCode actualResponseCode) = await motHttpClient.GetMotHistoryAsync(regNumber);

            //Assert
            Assert.Equal(HttpStatusCode.OK, actualResponseCode);
            Assert.Equal(expected, actualCar.Make);
            mockMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
        
        [Fact]
        public async Task WhenARegistrationIsNotFound_ShouldReturnANullCarAndNotFoundCode()
        {

            //Arrange
            var regNumber = "111111";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            MotHttpClient motHttpClient = new MotHttpClient(httpClient);

            //Act
            (Car actualCar, HttpStatusCode actualResponseCode) = await motHttpClient.GetMotHistoryAsync(regNumber);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, actualResponseCode);
            Assert.Null(actualCar);
            mockMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task WhenAnExceptionIsThrownFromMotApi_ShouldReturnANullCarAndInternalServerErrorCode()
        {

            //Arrange
            var regNumber = "111111";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                ) 
                .Throws(new HttpRequestException("Test Exception"))
                .Verifiable();

            var httpClient = new HttpClient(mockMessageHandler.Object);
            MotHttpClient motHttpClient = new MotHttpClient(httpClient);

            //Act
            (Car actualCar, HttpStatusCode actualResponseCode) = await motHttpClient.GetMotHistoryAsync(regNumber);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, actualResponseCode);
            Assert.Null(actualCar);
            mockMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

    }
}
