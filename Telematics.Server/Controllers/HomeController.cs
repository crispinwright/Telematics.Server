using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Modules;
using Telematics.Server.ServiceLayer;

namespace Telematics.Server.Controllers
{
	
    public class HomeController : Controller
    {
		private readonly IUserService _userService;

	    public HomeController(IUserService userService)
	    {
		    this._userService = userService;
	    }

	    public HomeController()
	    {
	    }

	    public ActionResult Index()
        {
	        ViewBag.Users = _userService.GetUsers();
            return View();
        }
    }
   
}
