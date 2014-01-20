using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Web.Helpers;
using System.Web.Http;
using Ninject.Extensions.Logging;
using Telematics.Server.Const;
using Telematics.Server.Data.DataModels;
using Telematics.Server.ServiceLayer;
using Telematics.Server.Data.Json;

using Telematics.Server.Models;
using User = Telematics.Server.Data.DataModels.User;

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
            GeoMain data = new GeoMain();
            data.Points = new List<Point>();
            for (int i = 0; i < datasource.Count-1; i++)
            {
                data.Points.Add(CreatePoint((double)datasource[i].Item1, (double)datasource[i].Item2, (double)datasource[i+1].Item1, (double)datasource[i+1].Item2));
            }
            Hubs.GeoHubContext.Instance().Send(data);
            return new string[] { "value1", "value2" };
        }

        private static List<Tuple<decimal,decimal>> datasource = new List<Tuple<decimal, decimal>>()
        {
            //{new Tuple<decimal, decimal>(-36.871616m, 174.709610m)},
            {new Tuple<decimal, decimal>(-36.872227m,174.705612m)},
//            {new Tuple<decimal, decimal>(-36.872543m,174.703705m)},
//            {new Tuple<decimal, decimal>(-36.872116m,174.699829m)},
//            {new Tuple<decimal, decimal>(-36.871994m,174.699036m)},
            {new Tuple<decimal, decimal>(-36.871964m,174.698746m)}
        };

        private static int pos = 0;


        // GET api/values/5
        public string Get(int id)
        {
            if (pos < datasource.Count)
            {
				GeoMain data = new GeoMain();
				data.Points = new List<Point>();
				data.Points.Add(CreatePoint(-36.872227, 174.705612, -36.872543, 174.703705));
				Hubs.GeoHubContext.Instance().Send(data);
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

        private Point CreatePoint(double fromLat, double fromLon, double toLat, double toLon)
        {
            return new Point()
            {
                Lat = -36.872543,
                Lon = toLon,
                Route = _geoService.RetrievePolyLineBetweenPoints(new Point()
                {
                    Lat = fromLat,Lon= fromLon
                }, new Point()
                {
                    Lat = toLat,
                    Lon = toLon
                })
            };
        }


        // POST api/values
       public HttpResponseMessage Post([FromBody]GeoMain geoData)
        {
           var data = Request.Content.ReadAsStringAsync().Result;

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

                    //at this point we need to push back to signalr
                    Hubs.GeoHubContext.Instance().Send(geoData);

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