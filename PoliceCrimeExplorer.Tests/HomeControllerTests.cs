using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PoliceCrimeExplorer.Controllers;
using PoliceCrimeExplorer.Services;

namespace PoliceCrimeExplorer.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private Mock<IPoliceDataClientService> _policeDataClientServiceMock;
        private Mock<IPoliceDataCalendarService> _policeDataCalendarServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _policeDataClientServiceMock = new Mock<IPoliceDataClientService>();
            _policeDataCalendarServiceMock = new Mock<IPoliceDataCalendarService>();
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(_loggerMock.Object, _policeDataClientServiceMock.Object, _policeDataCalendarServiceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Index_ReturnsNotNull()
        {
            // Arrange
            var controller = new HomeController(_loggerMock.Object, _policeDataClientServiceMock.Object, _policeDataCalendarServiceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetPoliceStreetCrimeDataInLocation_ReturnsNotNull()
        {
            // Arrange
            var controller = new HomeController(_loggerMock.Object, _policeDataClientServiceMock.Object, _policeDataCalendarServiceMock.Object);
            string latitudeGpsCord = "51.509865";
            string longitudeGpsCord = "-0.118092";
            DateTime searchDate = DateTime.Now;

            // Act
            var result = await controller.GetPoliceStreetCrimeDataInLocation(latitudeGpsCord, longitudeGpsCord, searchDate);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}