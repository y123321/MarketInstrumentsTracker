using MarketsTracker.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarketsTracker.Model
{
    public class RegistrationRequest:User
    {
        [Required]
        public string Password { get; set; }
        
    }
}