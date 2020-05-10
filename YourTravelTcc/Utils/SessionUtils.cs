﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace YourTravelTcc.Utils
{
    /// <summary>
    /// Class that contains helper methods to serializa values to the session.
    /// </summary>
    public static class SessionUtils
    {
        public static void SetObject( this ISession session, string key, object value )
        {
            session.SetString( key, JsonConvert.SerializeObject( value ) );
        }

        public static T GetObject<T>( this ISession session, string key )
        {
            var value = session.GetString( key );
            return value == null ? default( T ) : JsonConvert.DeserializeObject<T>( value );
        }

        public static void SetBoolean( this ISession session, string key, bool value )
        {
            session.Set( key, BitConverter.GetBytes( value ) );
        }

        public static bool? GetBoolean( this ISession session, string key )
        {
            var data = session.Get( key );
            if( data == null )
            {
                return null;
            }
            return BitConverter.ToBoolean( data, 0 );
        }

        public static void SetDouble( this ISession session, string key, double value )
        {
            session.Set( key, BitConverter.GetBytes( value ) );
        }

        public static double? GetDouble( this ISession session, string key )
        {
            var data = session.Get( key );
            if( data == null )
            {
                return null;
            }
            return BitConverter.ToDouble( data, 0 );
        }
    }
}
