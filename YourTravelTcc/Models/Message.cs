using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YourTravelTcc.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public String Text { get; set; }
        public int SenderID { get; set; }
        public int ReciverID { get; set; }
    }
}
