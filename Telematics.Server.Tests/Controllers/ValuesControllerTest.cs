using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Web.Common;
using Telematics.Server;
using Telematics.Server.Controllers;
using Telematics.Server.Data.Json;
using Telematics.Server.Models;
using Telematics.Server.NinjectUtils;
using Telematics.Server.ServiceLayer;

namespace Telematics.Server.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        private IGeoService _geoService;


        [TestMethod]
        public void Get()
        {
            // Arrange
            GeoController controller = new GeoController();

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
            GeoController controller = new GeoController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            GeoController controller = new GeoController();
            var data = @"{
    'sendTime': '2013-11-14T18:16:26.140625Z',
       'UserID': 6,
    'DeviceID': 'DOTA',
    'VehicleID': 2,
      'Points': [
        {
          'Lon': 81.13,
          'Lat': 175.0405582,
          'Speed': 130,
          'UTCTime': '2013-11-14T18:16:26.140625Z'
        },
        {
          'Lon': 81.13,
          'Lat': 175.0405582,
          'Speed': 85,
          'UTCTime': '2013-11-14T18:16:26.140625Z'
        },
        {
         'Lon': 81.13,
          'Lat': 175.0405582,
          'Speed': 88,
          'UTCTime': '2013-11-14T18:16:26.140625Z'
        },
        {
          'Lon': 81.13,
          'Lat': 175.0405582,
          'Speed': 92,
          'UTCTime': '2013-11-14T18:16:26.140625Z'
        }
      ]
  }

";
            // Act
            controller.Post(Json.Decode<GeoMain>(data));

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            GeoController controller = new GeoController();

            // Act

           

            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            GeoController controller = new GeoController();

            // Act
            controller.Delete(5);

            // Assert
        }

        
    }
}
