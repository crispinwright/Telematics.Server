using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telematics.Server.Models;
using Telematics.Server.DataModels;
namespace Telematics.Server.Controllers
{
    public class ValuesController : ApiController
    {
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
        public void Post([FromBody]Car value)
        {
            var context = new geoEntities();

            var user = new User();

            var vehicle = new UserVehicle();
            var vehicleSpeedPoint = new VehicleSpeed();

            user.DeviceID = value.Make + "asfdaasdadssd";
            context.Users.Add(user);

            vehicle.UserID = user.ID;
            vehicle.Name = "TestMeMore";
            user.UserVehicles.Add(vehicle);

            vehicleSpeedPoint.VehicleID = vehicle.ID;

            vehicleSpeedPoint.Lat = 51.11m;
            vehicleSpeedPoint.Lon = 24.22m;
            vehicleSpeedPoint.Speed = 54;

            user.VehicleSpeeds.Add(vehicleSpeedPoint);

            context.SaveChanges();

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