using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;
using Microsoft.AspNetCore.Http;
using YourTravelTcc.Utils;

namespace YourTravelTcc.Controllers
{
    /// <summary>
    /// Controller for Guide related functions.
    /// </summary>
    public class GuideController : Controller
    {
        private readonly MessageContext _messageContext;

        public GuideController( MessageContext messageContext )
        {
            this._messageContext = messageContext;
        }
        /// <summary>
        /// Redirect to GetMessages.
        /// </summary>
        /// <returns>A view with the Guide messages.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction( "GetMessages", "Guide" );
        }

        /// <summary>
        /// Get Guide's messages.
        /// </summary>
        /// <returns>A view with the messages.</returns>
        [HttpGet]
        public IActionResult GetMessages()
        {
            //Get this session Guide.
            var sessionGuide = HttpContext.Session.GetObject<Guide>( "Guide-" + HttpContext.Session.Id );
            var sessionPersonGuide = HttpContext.Session.GetObject<Person>( "PersonGuide-" + HttpContext.Session.Id );

            //Get Guide's messages.
            var tableJoin = ( from message in this._messageContext.Message
                              join traveler in this._messageContext.Traveler on message.TravelerID equals traveler.ID
                              join personTraveler in this._messageContext.Person on traveler.ID equals personTraveler.ID
                              where message.GuideID.Equals( sessionGuide.ID )
                              select new Message
                              {
                                  ID = message.ID,
                                  Text = message.Text,
                                  TravelerID = traveler.ID,
                                  GuideID = sessionGuide.ID,
                                  Guide = sessionPersonGuide,
                                  Traveler = personTraveler
                              } ).AsEnumerable();

            return View( tableJoin );
        }

        /// <summary>
        /// Load the SendMessage for a traveler page.
        /// </summary>
        /// <param name="travelerID">ID of the traveler we wish to send a message.</param>
        /// <returns>SendMessage for a traveler page.</returns>
        [HttpGet]
        public IActionResult SendMessage( int travelerID )
        {
            var message = new Message()
            {
                GuideID = HttpContext.Session.GetObject<Guide>( "Guide-" + HttpContext.Session.Id ).ID,
                TravelerID = travelerID
            };

            return View( message );
        }

        /// <summary>
        /// Post for sending a message to a traveler.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>Guide's messages page.</returns>
        [HttpPost]
        public IActionResult SendMessage( Message message )
        {
            message.GuideID = HttpContext.Session.GetObject<Guide>( "Guide-" + HttpContext.Session.Id ).ID;
            message.ID = new Random().Next( 1, int.MaxValue );

            this._messageContext.Add( message );
            this._messageContext.SaveChanges();

            return RedirectToAction( "GetMessages" );
        }
    }
}
