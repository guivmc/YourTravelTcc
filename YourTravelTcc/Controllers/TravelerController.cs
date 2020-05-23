using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;
using YourTravelTcc.Models.Enum;
using YourTravelTcc.Utils;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller for Traveler related functions.
    /// </summary>
    public class TravelerController : Controller
    {
        private readonly GuideContext _guideContext;
        private readonly MessageContext _messageContext;

        /// <summary>
        /// Default
        /// </summary>
        public TravelerController( GuideContext guideContext, MessageContext messageContext )
        {
            this._guideContext = guideContext;
            this._messageContext = messageContext;
        }

        /// <summary>
        /// Load the Traveler HomePage.
        /// </summary>
        /// <returns>Traveler HomePage.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search for guides in a given country.
        /// </summary>
        /// <param name="location">Country where to find the guides.</param>
        /// <returns>A page with a list of guides.</returns>
        [HttpPost]
        public IActionResult SearchForGuidesInLocation( String location )
        {
            //Get country that is being searched.
            CountryID countryID;
            Enum.TryParse( location, out countryID );

            //Join guide with person table and search for country, return a enumerable of guides.
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

            return View( "GuidesFound", tableJoin );
        }

        /// <summary>
        /// Get Traveler's messages.
        /// </summary>
        /// <returns>A view with the messages.</returns>
        [HttpGet]
        public IActionResult GetMessages()
        {
            //Get this session guide.
            var sessionTraveler = HttpContext.Session.GetObject<Traveler>( "Traveler-" + HttpContext.Session.Id );
            var sessionPersonTraveler = HttpContext.Session.GetObject<Person>( "Persontraveler-" + HttpContext.Session.Id );

            //Get Trvaler's messages.
            var tableJoin = ( from message in this._messageContext.Message
                              join guide in this._messageContext.Guide on message.GuideID equals guide.ID
                              join personGuide in this._messageContext.Person on guide.ID equals personGuide.ID
                              where message.TravelerID.Equals( sessionTraveler.ID )
                              select new Message
                              {
                                  ID = message.ID,
                                  Text = message.Text,
                                  TravelerID = sessionTraveler.ID,
                                  GuideID = guide.ID,
                                  Guide = personGuide,
                                  Traveler = sessionPersonTraveler
                              } ).AsEnumerable();

            return View( tableJoin );
        }

        /// <summary>
        /// Load the SendMessage for a guide page.
        /// </summary>
        /// <param name="guideID">ID of the guide we wish to send a message.</param>
        /// <returns>SendMessage for a guide page.</returns>
        [HttpGet]
        public IActionResult SendMessage( int guideID )
        {
            var message = new Message()
            {
                GuideID = guideID,
                TravelerID = HttpContext.Session.GetObject<Traveler>( "Traveler-" + HttpContext.Session.Id ).ID
            };

            return View( message );
        }

        /// <summary>
        /// Post for sending a message to a guide.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>Traveler's messages page.</returns>
        [HttpPost]
        public IActionResult SendMessage( Message message )
        {
            message.TravelerID = HttpContext.Session.GetObject<Traveler>( "Traveler-" + HttpContext.Session.Id ).ID;
            message.ID = new Random().Next( 1, int.MaxValue );

            this._messageContext.Add( message );
            this._messageContext.SaveChanges();

            return RedirectToAction( "GetMessages" );
        }
    }
}