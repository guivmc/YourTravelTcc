using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;
using YourTravelTcc.Models.Enum;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller for Traveler related functions.
    /// </summary>
    public class TravelerController : Controller
    {
        private readonly GuideContext _guideContext;

        private readonly PersonContext _personContext;

        /// <summary>
        /// Default
        /// </summary>
        public TravelerController(PersonContext personContext, GuideContext guideContext)
        {
            this._personContext = personContext;
            this._guideContext = guideContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchForGuidesInLocation(String location)
        {
            //Get country that is being searched.
            CountryID countryID;
            Enum.TryParse( location, out countryID );

            //var personList = new List<Person>();
            //personList.AddRange( this._personContext.Person.ToList() );

            //var tableJoin = this._guideContext.Guide.Join( this._personContext.Person, g => g.ID, p => p.ID, ( g, p ) => new
            //{
            //    g.ID,
            //    g.CompanyID,
            //    g.Rating,
            //    g.Available,
            //    PersonData = p
            //} ).ToList();

            //this._guideContext.Guide.ToList().Where( g => g.personData.CountryID.Equals( countryID ) );

            //var guides = 1;

            return RedirectToAction( "Index", "Traveler" );
        }
    }
}