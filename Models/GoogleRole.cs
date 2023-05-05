namespace AuthGoogleTest.Models
{
    public class GoogleRole
    {
        public GoogleRole(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public static explicit operator string(GoogleRole role)
        {
            return role.Name;
        }

    }
}
