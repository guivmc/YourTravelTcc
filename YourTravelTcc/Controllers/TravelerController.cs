using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller for Traveler related functions.
    /// </summary>
    public class TravelerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchForGuidesInLocation(String location)
        {
            return View();
        }
    }
}