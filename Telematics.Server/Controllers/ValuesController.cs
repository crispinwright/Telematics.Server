using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
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
        public void Post([FromBody]GEOMain geoData)
        {
            var context = new geoEntities();


            //var user = context.Users.Where(x => x.ID == geoData.PointTable.UserID);
            

            foreach (var point in geoData.PointTable.Points)
            {
                Debug.WriteLine(point.Lat);
                Debug.WriteLine(point.Lon);
                Debug.WriteLine(point.Speed);
                Debug.WriteLine(point.UTCTime);

                context.VehicleSpeeds.Add( 
                    new VehicleSpeed 
                    {
                        Lat = point.Lat, 
                        Lon = point.Lon,
                        Speed = point.Speed,
                        UserID = geoData.PointTable.UserID,
                        VehicleID = geoData.PointTable.VehicleID 
                    });    
            }

            context.SaveChanges();
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