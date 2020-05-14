﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourTravelTcc.Models;
using YourTravelTcc.Models.Context;

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

        public IActionResult Index()
        {
            var message1 = new Message() { Text = "Mensagem test 1." };
            var message2 = new Message() { Text = "Mensagem test 2." };

            var messages = new List<Message>();
            messages.Add( message1 );
            messages.Add( message2 );

            return View( messages );
        }
    }
}