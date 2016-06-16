using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTC.Models
{
    public class Attractions
    {
        [Key]
        public int AttractionId { get; set; }
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Image { get; set; }
    }
}
