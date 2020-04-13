using System.ComponentModel.DataAnnotations;

namespace MarketsTracker.Model
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}