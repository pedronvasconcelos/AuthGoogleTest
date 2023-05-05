namespace AuthGoogleTest.Models
{
    public class GoogleResponseLogin
    {
       

        public string? AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public Guid Id { get; set; }

    }
}
