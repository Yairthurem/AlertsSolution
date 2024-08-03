using Microsoft.EntityFrameworkCore;

namespace FlightAlerts.Modules
{
    public class Alert
    {        
        public Guid AlertId { get; set; }     
        public string UserName { get; set; } = "";        
        [FlightAbbrevationValid(ErrorMessage = "FlightAbbrevation must contain exactly 6 latin characters as a combination of the origin and destination F.E. PARMAD.")]
        public string FlightAbbrevation { get; set; } = "";        
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }        
        public bool IsReturn { get; set; }
        [Precision(18, 2)]
        [GreaterThan(0)]
        public decimal RequestedPrice { get; set; }

        //public decimal? LastAlertPrice { get; set; } 

        // Optional field to be Added after the alert is fulfilled
        // to indicate that the  alert was sent and for any cheaper alerts to come

    }
}