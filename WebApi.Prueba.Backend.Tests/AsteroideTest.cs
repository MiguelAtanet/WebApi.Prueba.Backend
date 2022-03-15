using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Prueba.Backend.Controllers.v1;
using Xunit;

namespace WebApi.Prueba.Backend.Tests
{
    public class AsteroideTest
    {
        private string jsonData { get; set; }
        private AsteroideNasaDto datosNasa { get; set; }
        public AsteroideTest() {
            var datos = new Dictionary<DateTime, List<Info>>();
            var info = new List<Info>() { new Info() { IsPotentiallyHazardousAsteroid = true,
                                                      Name = "Prueba",
                                                      EstimatedDiameter = new EstimatedDiameter() {
                                                                                    Kilometers = new Kilometers() {
                                                                                        EstimatedDiameterMin = 0.1455,
                                                                                        EstimatedDiameterMax = 0.1788 }
                                                      },
                                                      CloseApproachData = new List<CloseApproachData>(){
                                                                                    new CloseApproachData() {
                                                                                        CloseApproachDate = "2022-03-16",
                                                                                        OrbitingBody = "Earth",
                                                                                        RelativeVelocity = new RelativeVelocity(){ KilometersPerHour = "5432.34"}
                                                                                    }
                                                      }}};
            datos.Add(new DateTime(), info);



            datosNasa = new AsteroideNasaDto() { ElementCount = 1, NearEarthObjects = datos };
            jsonData = JsonConvert.SerializeObject(datosNasa);
        }

        [Theory]
        [InlineData("Earth")]
        public async Task TestGetTop3Async_Is_Succefully(string planet)
        {
            //arrage
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var logger = new Mock<ILogger<AsteroideController>>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                    ItExpr.IsAny<HttpRequestMessage>(),
                                                    ItExpr.IsAny<CancellationToken>())
                                             .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => new HttpResponseMessage() {StatusCode = System.Net.HttpStatusCode.OK,
                                             Content = new StringContent(jsonData)});
                                                              

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            var asteroideController = new AsteroideController(logger.Object, httpClientFactory.Object);
            httpClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(client);

            //Act
            var result = await asteroideController.GetTop3Async(planet) as ObjectResult;

            //Assert
            Assert.NotNull(result.Value);
        }

        [Theory]
        [InlineData("Earth")]
        public async Task TestGetTop3Async_Is_NoContent(string planet)
        {
            //arrage
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var logger = new Mock<ILogger<AsteroideController>>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                    ItExpr.IsAny<HttpRequestMessage>(),
                                                    ItExpr.IsAny<CancellationToken>())
                                             .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => new HttpResponseMessage()
                                             {
                                                 StatusCode = System.Net.HttpStatusCode.OK,
                                                 Content = new StringContent("")
                                             });


            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://api.nasa.gov/");
            var asteroideController = new AsteroideController(logger.Object, httpClientFactory.Object);
            httpClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(client);

            //Act
            var result = await asteroideController.GetTop3Async(planet) as ObjectResult;

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("")]
        public async Task TestGetTop3Async_Is_Param_IsNullOrEmtpy(string planet)
        {
            //arrage
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var logger = new Mock<ILogger<AsteroideController>>();
         
            var asteroideController = new AsteroideController(logger.Object, httpClientFactory.Object);

            //Act
            var result = await asteroideController.GetTop3Async(planet) as ObjectResult;

            //Assert
            Assert.Equal("Tiene que rellenar un nombre de un planeta",result.Value);
        }

        [Theory]
        [InlineData("Earth")]
        public async Task TestGetTop3Async_InternalServerError(string planet)
        {
            //arrage
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var logger = new Mock<ILogger<AsteroideController>>();

            var asteroideController = new AsteroideController(logger.Object, httpClientFactory.Object);

            //Act
            var result = await asteroideController.GetTop3Async(planet) as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode,StatusCodes.Status500InternalServerError);
        }

    }
}
