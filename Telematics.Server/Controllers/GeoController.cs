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
using Telematics.Server.Models;

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
            Telematics.Server.Hubs.GeoHubContext.Instance().Send(new
            {
                CarPlate = "dsa",
                Lat = -36.8730m,
                Long = 174.7550m,
                Time = DateTime.Now
            });
            return "value";
        }
        

        // POST api/values
       public HttpResponseMessage Post([FromBody]GeoMain geoData)
        {
            try
            {
                if (geoData == null)
                {
                    _logger.Info(() => "GeoData is null", WindowsEventID.GenericTelematicsEvent);

                    var zeroResponse = Request.CreateResponse<RecordsAdded>(System.Net.HttpStatusCode.OK, new RecordsAdded { Count = 0 });
                    return zeroResponse;
                }
                else
                {
                    _logger.Info(() => "About to Post points", WindowsEventID.GenericTelematicsEvent);

                    var recordsAdded = _geoService.AddGeoUserPoints(geoData);

                    var validResponse = Request.CreateResponse<RecordsAdded>(System.Net.HttpStatusCode.OK, new RecordsAdded { Count = recordsAdded });

                    return validResponse;
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