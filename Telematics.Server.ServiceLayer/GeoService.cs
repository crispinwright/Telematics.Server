using System;
using System.Collections.Generic;
using System.Linq;
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

	    public string RetrievePolyLineBetweenPoints(Point origin, Point dest)
	    {
			/*http://maps.googleapis.com/maps/api/directions/json?origin=-36.8730,174.7550&destination=-36.8745,174.7589&sensor=false*/
		    var url = string.Format("http://maps.googleapis.com/maps/api/directions/json?sensor=false&origin={0},{1}&destination={2},{3}", origin.Lat, origin.Lon, dest.Lat, dest.Lon);
			HttpClient cl = new HttpClient();
			var res=cl.GetAsync(url);
		    var data = res.Result.Content.ReadAsStringAsync().Result;
		    dynamic x = Json.Decode(data);

		    if (x.routes == null || x.routes.Count == 0)
			    return "";
			return x.routes[0].overview_polyline.points;
	    }

        public int AddGeoUserPoints(GeoMain geoData)
        {
            var context = new geoEntities();
            //var user = context.Users.Where(x => x.ID == geoData.PointTable.UserID);
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
                        DeviceID = geoData.DeviceID
                    })
                    
                    );
            return context.SaveChanges();
        }
    }
}
