using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telematics.Server.Data.Json
{
    public class GeoMain
    {
        public DateTime sendTime { get; set; }
        public int UserID { get; set; }
        public string DeviceID { get; set; }
        public int VehicleID { get; set; }
        public List<Point> Points { get; set; }
    }

        public class Point
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public int Speed { get; set; }
        public DateTime UTCTime { get; set; }
		public string Route { get; set; }
    }

	public class User
	{
		public int UserID { get; set; }
		string Name { get; set; }
	}

}
