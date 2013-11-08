﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telematics.Server.Data.Json
{
    public class Point
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public int Speed { get; set; }
        public DateTime UTCTime { get; set; }
    }

    public class GeoMain
    {
        public DateTime sendTime { get; set; }
        public int UserID { get; set; }
        public string DeviceID { get; set; }
        public int VehicleID { get; set; }
        public List<Point> Points { get; set; }
    }
}