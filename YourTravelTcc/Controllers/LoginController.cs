using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller esponsible for Logins and SingUps.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        private readonly PersonContext _personContext;

        private readonly TravelerContext _travelerContext;

        private readonly GuideContext _guideContext;

        public LoginController( ILogger<LoginController> logger, PersonContext personContext, TravelerContext travelerContext, GuideContext guideContext)
        {
            this._logger = logger;
            this._personContext = personContext;
            this._travelerContext = travelerContext;
            this._guideContext = guideContext;
        }

        [HttpPost]
        public IActionResult Login( Person person )
        {
            if( ModelState.IsValid )
            {
                //try
                //{
                    var login = this._personContext.Person.Single( p => p.Email.Equals( person.Email ) && p.Password.Equals( person.Password ) );

                    var guide = this._guideContext.Guide.SingleOrDefault( g => g.ID.Equals( login.ID) );

                    if( guide != null )
                        return RedirectToAction();

                    var traveler = this._travelerContext.Traveler.SingleOrDefault( t => t.ID.Equals( login.ID ) );

                    if( traveler != null )
                        return RedirectToAction();


                    return BadRequest();
                //}
                //catch( InvalidOperationException )
                //{
                //    return BadRequest();
                //}
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region SingUp
        [HttpGet]
        public IActionResult SingUpAsTraveler()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingUpAsTraveler( Traveler traveler )
        {
            if( ModelState.IsValid )
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult SingUpAsGuide()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingUpAsGuide( Guide guide )
        {
            if( ModelState.IsValid )
            {
                return Ok();
            }

            return BadRequest();
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}