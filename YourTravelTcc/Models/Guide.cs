using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourTravelTcc.Models
{
    /// <summary>
    /// Guide model, inherites from person.
    /// A guide is the user that will sell its services on the platform.
    /// Person iharitance o attribute personData
    /// </summary>
    public class Guide 
    {
        [Key]
        [ForeignKey("ID")]
        public int ID { get; set; }
        public int? CompanyID { get; set; }
        public double Rating { get; set; }
        public bool Available { get; set; }

        [NotMapped]
        public Person personData { get; set; } 

    }
}
