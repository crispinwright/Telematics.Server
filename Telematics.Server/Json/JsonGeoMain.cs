using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telematics.Server.Json
{
public class Point
{
    public string Lon { get; set; }
    public string Lat { get; set; }
    public string Speed { get; set; }
    public string UTCTime { get; set; }
}

public class PointTable
{
    public string UserID { get; set; }
    public string DeviceID { get; set; }
    public string VehicleID { get; set; }
    public List<Point> Points { get; set; }
}

public class GEOMain
{
    public string sendTime { get; set; }
    public PointTable PointTable { get; set; }
}

public class RootObject
{
    public GEOMain GEOMain { get; set; }
}
}