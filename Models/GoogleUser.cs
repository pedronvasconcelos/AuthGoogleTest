namespace AuthGoogleTest.Models
{
    public class GoogleUser
    {
        public GoogleUser( string email, string name, string firstName, string familyName, GoogleRole role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            FirstName = firstName;
            FamilyName = familyName;
            Role = role;
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

        public GoogleRole Role { get; set; }

    }
}
