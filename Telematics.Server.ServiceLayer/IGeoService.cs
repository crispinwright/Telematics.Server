using Telematics.Server.Data.Json;

namespace Telematics.Server.ServiceLayer
{
    public interface IGeoService
    {
        void AddGeoUserPoints(Telematics.Server.Data.Json.GeoMain geoData);
    }
}