using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Web.Http;
using Telematics.Server.Data.DataModels;
using Telematics.Server.ServiceLayer;

namespace Telematics.Server.Controllers
{
    public class GeoController : ApiController
    {

        private readonly IGeoService _geoService = new GeoService();


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
        public void Post([FromBody]GEOMain geoData)
        {

            _geoService.AddGeoUserPoints(geoData);

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