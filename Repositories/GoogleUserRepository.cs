using AuthGoogleTest.Models;
using AuthGoogleTest.Services;

namespace AuthGoogleTest.Repositories
{
    public interface IGoogleUserRepository
    {
        GoogleUser GetUserByEmail(string email);
        void RegisterNewUser(GoogleUser newUser);
    }

    public class GoogleUserRepository : IGoogleUserRepository
    {
        List<GoogleUser> _users = new List<GoogleUser> {
        new GoogleUser(
             "jttest@gmail.com.br",  "Jax Teller",
            "Jax", "Teller", new GoogleRole(1, "Administrator"))
    };

        public GoogleUser GetUserByEmail(string email)
        {
            return _users.Where(x => x.Email == email).FirstOrDefault();
        }

        public void RegisterNewUser(GoogleUser newUser)
        {
            _users.Add(newUser);
        }
    }
}
