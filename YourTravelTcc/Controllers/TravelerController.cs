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

        /// <summary>
        /// Default
        /// </summary>
        public TravelerController(GuideContext guideContext )
        {
            this._guideContext = guideContext;
        }

        /// <summary>
        /// Load the Traveler HomePage.
        /// </summary>
        /// <returns>Traveler HomePage.</returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchForGuidesInLocation( String location )
        {
            //Get country that is being searched.
            CountryID countryID;
            Enum.TryParse( location, out countryID );

            var tableJoin = ( from guide in this._guideContext.Guide
                              join person in this._guideContext.Person on guide.ID equals person.ID
                              where person.CountryID.Equals( countryID )
                              select new Guide
                              {
                                  ID = guide.ID,
                                  CompanyID = guide.CompanyID,
                                  Rating = guide.Rating,
                                  Available = guide.Available,
                                  PersonData = person
                              } ).AsEnumerable();

            return View("GuidesFound", tableJoin );
        }
    }
}