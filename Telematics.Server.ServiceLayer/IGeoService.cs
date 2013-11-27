using System.Threading.Tasks;
using Telematics.Server.Data.Json;

namespace Telematics.Server.ServiceLayer
{
    public interface IGeoService
    {
        int AddGeoUserPoints(Telematics.Server.Data.Json.GeoMain geoData);
	    string RetrievePolyLineBetweenPoints(Point origin, Point dest);
    }
}