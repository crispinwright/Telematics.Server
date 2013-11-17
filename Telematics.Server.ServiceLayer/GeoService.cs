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
