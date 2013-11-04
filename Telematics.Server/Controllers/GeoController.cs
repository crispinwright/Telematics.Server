using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Web.Http;
using Ninject.Extensions.Logging;
using Telematics.Server.Const;
using Telematics.Server.Data.DataModels;
using Telematics.Server.ServiceLayer;
using Telematics.Server.Json;

namespace Telematics.Server.Controllers
{
    public class GeoController : ApiController
    {


        private readonly IGeoService _geoService;
        private readonly ILogger _logger;

        public GeoController(IGeoService geoService, ILogger logger)
        {
            _logger = logger;
            _geoService = geoService;
        }

        public GeoController()
        {
            
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(HttpRequestMessage request)
        {
            //[FromBody]Json.GEOMain geoData
            try
            {
                //Json.GEOMain  geoData = 
                if (geoData.PointTable == null)
                    _logger.Info(() => "GeoData is null", WindowsEventID.GenericTelematicsEvent);
                else
                    _logger.Info(() => "About to Post points", WindowsEventID.GenericTelematicsEvent);
                //_geoService.AddGeoUserPoints(geoData);

            }
            catch (Exception e)
            {
                _logger.Info(() => "Could not load configuration file: \r\nReason: " + e.Message, WindowsEventID.TelematicsConfiguration);

                throw;
            }

        }

        private void addStuff()
        {
            var vehicle = new UserVehicle();
            var vehicleSpeedPoint = new VehicleSpeed();
            var context = new geoEntities();
            var user = new User();
            context.Users.Add(user);

            vehicle.UserID = user.ID;
            vehicle.Name = "TestMeMore";
            user.UserVehicles.Add(vehicle);


            vehicleSpeedPoint.VehicleID = vehicle.ID;

            var y = context.Users.Select(x => x.ID == 1).FirstOrDefault();

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            Console.WriteLine("GOT A PUT");
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}