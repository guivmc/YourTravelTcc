using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;
using Microsoft.AspNetCore.Http;
using YourTravelTcc.Utils;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller responsible for Logins and SignUps.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        private readonly PersonContext _personContext;

        private readonly TravelerContext _travelerContext;

        private readonly GuideContext _guideContext;

        /// <summary>
        /// Default contructor.
        /// </summary>
        public LoginController( ILogger<LoginController> logger, PersonContext personContext, TravelerContext travelerContext, GuideContext guideContext )
        {
            this._logger = logger;
            this._personContext = personContext;
            this._travelerContext = travelerContext;
            this._guideContext = guideContext;
        }

        /// <summary>
        /// Serach for user on the data base, if it's a guide load guide homepage and put the guide on session, 
        /// if it's a traveler load traveler homepage and put the traveler on session, 
        /// else return an error.
        /// </summary>
        /// <param name="person">Model to be serached for.</param>
        /// <returns>If it's a guide go to guide homepage, if it's a traveler go to traveler homepage, else return an error.</returns>
        [HttpPost]
        public IActionResult Login( Person person )
        {
            if( ModelState.IsValid )
            {
                try
                {
                    var login = this._personContext.Person.Single( p => p.Email.Equals( person.Email ) && p.Password.Equals( person.Password ) );

                    var guide = this._guideContext.Guide.SingleOrDefault( g => g.ID.Equals( login.ID ) );

                    if( guide != null )
                    {
                        HttpContext.Session.SetObject( "Guide-" + guide.ID, guide );
                        return RedirectToAction( "Index", "Guide" );
                    }

                    var traveler = this._travelerContext.Traveler.SingleOrDefault( t => t.ID.Equals( login.ID ) );

                    if( traveler != null )
                    {
                        HttpContext.Session.SetObject( "Traveler-" + traveler.ID, traveler );
                        return RedirectToAction( "Index", "Traveler" );
                    }


                    return BadRequest();
            }
                catch( InvalidOperationException )
            {
                return BadRequest();
            }
        }

            return BadRequest();
        }

        /// <summary>
        /// Load Homepage.
        /// </summary>
        /// <returns>Homepage.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region SignUp
        #region Traveler
        /// <summary>
        /// Load Sign up as a traveler homepage.
        /// </summary>
        /// <returns>Sign up as a traveler homepage.</returns>
        [HttpGet]
        public IActionResult SignUpAsTraveler()
        {
             return View();
        }

        /// <summary>
        /// Register a traveler and a person on the data base, load the traveler homepage and put traveler on session.
        /// </summary>
        /// <param name="traveler">Traveler model to be registered.</param>
        /// <returns>Load the traveler homepage.</returns>
        [HttpPost]
        public IActionResult SignUpAsTraveler( Traveler traveler )
        {
            if( ModelState.IsValid )
            {
                //Generate a new radom id.
                int id = new Random().Next(1, int.MaxValue);

                traveler.ID = id;

                traveler.personData.ID = id;


                //Add and save the changes on the data base.
                this._personContext.Add( traveler.personData );

                this._personContext.SaveChanges();

                this._travelerContext.Add( traveler );

                this._travelerContext.SaveChanges();


                HttpContext.Session.SetObject( "Traveler-" + traveler.ID, traveler );

                return RedirectToAction( "Index", "Traveler" );
            }

            return BadRequest();
        }
        #endregion

        #region Guide
        /// <summary>
        /// Load Sign up as a guide page.
        /// </summary>
        /// <returns>Sign up as a guide page</returns>
        [HttpGet]
        public IActionResult SignUpAsGuide()
        {
            return View();
        }

        /// <summary>
        /// Register a guide and a person on the data base, load the guide homepage and put guide on session.
        /// </summary>
        /// <param name="guide">Guide model to be registered.</param>
        /// <returns>Load the guide homepage.</returns>
        [HttpPost]
        public IActionResult SignUpAsGuide( Guide guide )
        {
            if( ModelState.IsValid )
            {
                //Generate a new radom id.
                int id = new Random().Next( 1, int.MaxValue );

                guide.ID = id;

                guide.PersonData.ID = id;


                //Add and save the changes on the data base.
                this._personContext.Add( guide.PersonData );

                this._personContext.SaveChanges();

                this._guideContext.Add( guide );

                this._guideContext.SaveChanges();


                HttpContext.Session.SetObject( "Guide-" + guide.ID, guide );

                return RedirectToAction( "Index", "Guide" );
            }

            return BadRequest();
        }
    
        #endregion
        #endregion

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        //public IActionResult Error()
        //{
        //    return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        //}
    }
}