using AuthGoogleTest.Models;
using AuthGoogleTest.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthGoogleTest.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IGoogleUserRepository _googleRepository;

        public AuthenticationService(IGoogleUserRepository googleRepository)
        {
            _googleRepository = googleRepository;
        }

        private async Task<ClaimsIdentity> GetUserClaims( GoogleUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, (string)user.Role)

            };


            var identityClaims = new ClaimsIdentity(claims);
            return identityClaims;
        }


        public async Task<GoogleResponseLogin> GenerateJwtToken(GoogleUser user)
        {

            var identityClaims = await GetUserClaims(user);
            var encodedToken = EncodeToken(identityClaims);

            return GetResponseLogin(encodedToken, user);
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secretaleatoryWith128Bits12312930@#321");
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "IssuerTest",
                Audience = "IssuerAudience",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }


        private GoogleResponseLogin GetResponseLogin(string encodedToken, GoogleUser user)
        {
            return new GoogleResponseLogin
            {
                AccessToken = encodedToken,
               ExpiresIn = TimeSpan.FromHours(1231231).TotalSeconds,
                Email = user.Email,
                Role = (string)user.Role,
                Id = user.Id,

            };
        }

    }

    public interface IAuthenticationService
    {
        Task<GoogleResponseLogin> GenerateJwtToken(GoogleUser user);
    }
}
