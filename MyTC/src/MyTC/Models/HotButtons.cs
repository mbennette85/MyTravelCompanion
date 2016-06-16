using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTC.Models
{
    public class HotButtons
    {
        [Key]
        public int ButtonId { get; set; }
        public int GenreId { get; set; }
        public string Translation { get; set; }
    }
}
