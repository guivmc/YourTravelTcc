using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller for Guide related functions.
    /// </summary>
    public class GuideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}