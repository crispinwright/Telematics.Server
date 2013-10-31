using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telematics.Server;
using Telematics.Server.Controllers;
using Telematics.Server.Models;

namespace Telematics.Server.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            GeoApiController controller = new GeoApiController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            GeoApiController controller = new GeoApiController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            GeoApiController controller = new GeoApiController();

            // Act
            //controller.Post(new Car{EngineSize = 2898,Make = "Toyota"});

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            GeoApiController controller = new GeoApiController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            GeoApiController controller = new GeoApiController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
