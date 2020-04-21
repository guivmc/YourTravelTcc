using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YourTravelTcc.Models
{
    /// <summary>
    /// Traveler model, inherites from person.
    /// A traveler is the user that will search for options to customize its trip.
    /// Person iharitance o attribute personData
    /// </summary>
    public class Traveler
    {
        [Key]
        [ForeignKey( "ID" )]
        public int ID { get; set; }

        [NotMapped]
        public Person personData { get; set; }
    }
}
