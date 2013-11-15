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
using Telematics.Server.Data.Json;

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

        private static List<Tuple<decimal,decimal>> datasource = new List<Tuple<decimal, decimal>>()
        {
            //{new Tuple<decimal, decimal>(-36.872227m,174.705612m)},
            {new Tuple<decimal, decimal>(-36.872543m,174.703705m)},
            {new Tuple<decimal, decimal>(-36.872116m,174.699829m)},
            {new Tuple<decimal, decimal>(-36.871994m,174.699036m)},
            {new Tuple<decimal, decimal>(-36.871964m,174.698746m)}
        };

        private static int pos = 0;


        // GET api/values/5
        public string Get(int id)
        {
            if (pos < datasource.Count)
            {
                Hubs.GeoHubContext.Instance().Send(new
                {
                    CarPlate = "dsa",
                    Lat = datasource[pos].Item1,
                    Long = datasource[pos].Item2,
                    Time = DateTime.Now
                });
                pos++;
                
            }
            else
            {
                pos = 0;
                Hubs.GeoHubContext.Instance().Send(new
                {
                    CarPlate = "dsa",
                    Lat = datasource[pos].Item1,
                    Long = datasource[pos].Item2,
                    Time = DateTime.Now
                });
            }
            return "value";
        }
        

        // POST api/values
       public void Post([FromBody]GeoMain geoData)
        {
            try
            {
                if (geoData == null)
                    _logger.Info(() => "GeoData is null", WindowsEventID.GenericTelematicsEvent);
                else
                {
                    _logger.Info(() => "About to Post points", WindowsEventID.GenericTelematicsEvent);
                    _geoService.AddGeoUserPoints(geoData);
                }

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