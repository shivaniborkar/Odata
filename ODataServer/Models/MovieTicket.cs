using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataServer.Models
{
    public class MovieTicket
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Language { get; set; }
        public string TypeOfMovie { get; set; }
        public int Time { get; set; }
       
    }
}