using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Telematics.Server.Hubs
{
    public class GeoHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }

    public class GeoHubContext
    {
        private readonly static Lazy<GeoHubContext> _instance = new Lazy<GeoHubContext>(
      () => new GeoHubContext(GlobalHost.ConnectionManager.GetHubContext<GeoHub>()));

        private IHubContext _context;

        public static GeoHubContext Instance()
        {
            return _instance.Value;
        }

        private GeoHubContext(IHubContext context)
        {
            _context = context;
        }

        public void Send(object data)
        {
            _context.Clients.All.sendGeoData(data);
        }
    }
}