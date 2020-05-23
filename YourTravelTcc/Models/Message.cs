using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourTravelTcc.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public String Text { get; set; }
        [ForeignKey( "ID" )]
        public int TravelerID { get; set; }
        [ForeignKey( "ID" )]
        public int GuideID { get; set; }
        [NotMapped]
        public Person Guide { get; set; } //To represente personal information of the Guide
        [NotMapped]
        public Person Traveler { get; set; } //To represente personal information of the Traveler
    }
}
