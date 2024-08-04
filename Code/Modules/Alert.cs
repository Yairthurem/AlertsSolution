using Microsoft.EntityFrameworkCore;

namespace FlightAlerts.Modules
{
    public class Alert
    {
        public Guid AlertId { get; set; }
        public string UserName { get; set; } = "";
        [FlightAbbrevationValid(ErrorMessage = "FlightAbbreviation must contain exactly 6 latin characters as a combination of the origin and destination F.E. PARMAD.")]
        public string FlightAbbreviation { get; set; } = "";
        [FutureDate]
        public DateTime DepartureDate { get; set; }        
        public bool IsReturn { get; set; }
        [FutureDate][LaterReturnDate]
        public DateTime? ReturnDate { get; set; }
        [Precision(18, 2)][GreaterThan(0)]
        public decimal RequestedPrice { get; set; }

        // Optional field to be Added after the alert is fulfilled
        // to indicate that the  alert was sent and for any cheaper alerts to come
        //public decimal? LastAlertPrice { get; set; } 
    }
}