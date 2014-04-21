using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Ninject.Modules;
using Telematics.Server.ServiceLayer;

namespace Telematics.Server.Controllers
{
	
    public class HomeController : Controller
    {
		private readonly IUserService _userService;
        private readonly IGeoService _geoService;

        public HomeController(IGeoService geoService, IUserService userService)
	    {
		    this._userService = userService;
            _geoService = geoService;
	    }

	    public HomeController()
	    {
	    }

	    public ActionResult Index()
        {
	     //   ViewBag.Users = _userService.GetUsers();
            return View();
        }

        public ActionResult Clear()
        {
            _geoService.Clear();
            return View("Debug", "_Layout", "job done");
        }

        public ActionResult Debug()
        {
            var url =
                "http://maps.googleapis.com/maps/api/directions/json?origin=-36.8730,174.7550&destination=-36.8745,174.7589&sensor=false";
	                
                var handler = new HttpClientHandler
                {
//                    CookieContainer = cookies,
//                    UseCookies = true,
                    UseDefaultCredentials = false,
                    //Credentials = ,
                    //Proxy = new WebProxy("http://w8dvaklpx01", false, new string[] { },new NetworkCredential("si554437","")),
                    UseProxy = true,
                };
                HttpClient cl = new HttpClient(handler);
                var res = cl.GetAsync(url);
	            var data = res.Result.Content.ReadAsStringAsync().Result;

                return View("Debug", "_Layout", data);
        }
    }
   
}
