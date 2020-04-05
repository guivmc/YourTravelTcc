using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourTravelTcc.Models
{
    /// <summary>
    /// Guide model.
    /// A guide is the user that will sell its services on the platform.
    /// </summary>
    public class Guide : Person
    {
        public int? CompanyID { get; set; }
        public float Rating { get; set; }
        public bool Available { get; set; }
    }
}
