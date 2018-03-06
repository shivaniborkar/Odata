
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataServer.Models
{
    public class Attendee
    {
        public int AttendeeId { get; set; }
        public string name { get; set; }
        public int age { get; set; }

        public int dummyProperty { get; set; }

    }
}