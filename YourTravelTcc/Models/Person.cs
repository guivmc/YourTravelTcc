using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YourTravelTcc.Models.Enum;

namespace YourTravelTcc.Models
{
    /// <summary>
    /// Base model for a Person.
    /// </summary>
    public class Person
    {
        [Key]
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String SurName { get; set; }
        public CountryID CountryID { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        /// <summary>
        /// Method for getting the persons full name.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.SurName;
            }
        }
    }
}
