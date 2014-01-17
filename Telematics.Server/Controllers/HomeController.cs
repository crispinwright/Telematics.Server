using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Modules;
using Telematics.Server.ServiceLayer;
using Ninject.Extensions.Logging;
using Telematics.Server.Const;

namespace Telematics.Server.Controllers
{
	
    public class HomeController : Controller
    {
		private readonly IUserService _userService;
        private readonly ILogger _logger;

	    public HomeController(IUserService userService, ILogger logger)
	    {
            this._logger = logger;
		    this._userService = userService;
	    }

	    public HomeController()
	    {
	    }

	    public ActionResult Index()
        {
	       // ViewBag.Users = _userService.GetUsers();
            return View();
        }

        public ActionResult LogSomething()
        {

            _logger.Error(() => "LOG IS FULL", WindowsEventID.TelematicsConfiguration);

            //ViewBag.Users = _userService.GetUsers();
            return View();
        }

    }
   
}
