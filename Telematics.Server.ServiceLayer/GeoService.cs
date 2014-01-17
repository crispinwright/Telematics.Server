using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Telematics.Server.Data.DataModels;
using Telematics.Server.Data.Json;

namespace Telematics.Server.ServiceLayer
{
    public class GeoService : IGeoService
    {

	    public  string RetrievePolyLineBetweenPoints(Point origin, Point dest)
	    {
	        try
	        {
	            /*http://maps.googleapis.com/maps/api/directions/json?origin=-36.8730,174.7550&destination=-36.8745,174.7589&sensor=false*/
	            var url =
	                string.Format(
	                    "http://maps.googleapis.com/maps/api/directions/json?sensor=false&origin={0},{1}&destination={2},{3}",
	                    origin.Lat, origin.Lon, dest.Lat, dest.Lon);
                var handler = new HttpClientHandler
                {
//                    CookieContainer = cookies,
//                    UseCookies = true,
                    UseDefaultCredentials = false,
                    //Credentials = ,
                   // Proxy = new WebProxy("http://w8dvaklpx01", false, new string[] { },new NetworkCredential("si554437","")),
                    UseProxy = true,
                };
                HttpClient cl = new HttpClient(handler);
                var res = cl.GetAsync(url);
	            var data = res.Result.Content.ReadAsStringAsync().Result;
	            dynamic x = Json.Decode(data);

                if (x.routes == null || x.routes.Length == null || x.routes.Length == 0)
	                return null;
	            return x.routes[0].overview_polyline.points;
	        }
	        catch (Exception ex)
	        {
	            Trace.WriteLine(ex.Message);
	            return null;
	        }

	    }

        public int AddGeoUserPoints(GeoMain geoData)
        {
            var context = new geoEntities();
            //gather the route information from google
            var orderedPoints = geoData.Points.OrderBy(x=>x.UTCTime).ToList();
            //get the last point added till now

            AddRouteInformationToPoints(geoData, context, orderedPoints);

            geoData.Points.ToObservable().Subscribe(i =>
                context.VehicleSpeeds.Add(
                    new VehicleSpeed
                    {
                        Lat = i.Lat,
                        Lon = i.Lon,
                        Speed = i.Speed,
                        UserID = geoData.UserID,
                        VehicleID = geoData.VehicleID,
                        UTCTime = i.UTCTime,
                        DeviceID = geoData.DeviceID,
                        Route = i.Route
                    })
                    );
            try
            {
                return context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                return 0;
            }
        }

        private void AddRouteInformationToPoints(GeoMain geoData, geoEntities context, List<Point> orderedPoints)
        {
            try
            {
                var lastPoint =
                    context.VehicleSpeeds.Where(x => x.VehicleID == geoData.VehicleID && x.DeviceID == geoData.DeviceID)
                        .OrderByDescending(x => x.UTCTime)
                        .FirstOrDefault();
                if (lastPoint != null)
                {
                    orderedPoints.Insert(0, new Point()
                    {
                        Lat = lastPoint.Lat ?? 0,
                        Lon = lastPoint.Lon ?? 0
                    });
                }

                //NOTE: in previous experimentation with google doing something like the below can lead to google rate limiting you, so doing this in a sequential fashion may fix this...
                var tasks = new List<Task>();
                for (int i = 1; i < orderedPoints.Count(); i++)
                {
                    int i1 = i;
                    tasks.Add(
                        Task.Factory.StartNew((
                            () =>
                            {
                                orderedPoints[i1].Route =
                                    RetrievePolyLineBetweenPoints(orderedPoints[i1 - 1], orderedPoints[i1]);
                            })));
                }
                
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
           
        }
    }
}
