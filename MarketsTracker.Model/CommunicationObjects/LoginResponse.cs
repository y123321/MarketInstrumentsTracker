namespace MarketsTracker.Model
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Scheme { get; set; }
        public string Redirect { get; set; }
    }
}