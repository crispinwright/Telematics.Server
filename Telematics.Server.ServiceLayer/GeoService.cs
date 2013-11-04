using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Telematics.Server.Data.DataModels;
using Telematics.Server.Data.Json;

namespace Telematics.Server.ServiceLayer
{
    public class GeoService : IGeoService
    {

        public void AddGeoUserPoints(Telematics.Server.Data.Json.GEOMain geoData)
        {
            var context = new geoEntities();
            //var user = context.Users.Where(x => x.ID == geoData.PointTable.UserID);
            geoData.PointTable.Points.ToObservable().Subscribe(i =>
                context.VehicleSpeeds.Add(
                    new VehicleSpeed
                    {
                        Lat = i.Lat,
                        Lon = i.Lon,
                        Speed = i.Speed,
                        UserID = geoData.PointTable.UserID,
                        VehicleID = geoData.PointTable.VehicleID
                    }));
            context.SaveChanges();
        }
    }
}
