using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketsTracker.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        public ICollection<Instrument> Instruments { get; set; } = new List<Instrument>();
    }
}