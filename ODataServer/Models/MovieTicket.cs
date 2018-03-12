using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataServer.Models
{
    public class MovieTicket
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int movieId { get; set; }
        public string movieName { get; set; }
        public string language { get; set; }
        public string typeOfMovie { get; set; }
        public int time { get; set; }
        public int availableTickets { get; set; }
        public int numOfTicketsToBuy { get; set; }
        
    }
}