using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTC.Models
{
    public class VisitedAttractions
    {
        public int VisitedId { get; set; }
        public int TravelerId { get; set; }
        public int AttractionId { get; set; }
        public string Comments { get; set; }
        public int AttractionRating { get; set; }
    }
}
