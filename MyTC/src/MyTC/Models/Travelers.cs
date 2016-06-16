using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTC.Models
{
    public class Travelers
    {
        public int TravelerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
        public int TravelRating { get; set; }
    }
}
