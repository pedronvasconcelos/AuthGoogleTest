using AuthGoogleTest.Models;
using AuthGoogleTest.Repositories;
using AuthGoogleTest.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AuthGoogleTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IGoogleUserRepository _googleUserRepository;
        private readonly IAuthenticationService _authService;

        public AuthController(IGoogleUserRepository googleUserRepository, IAuthenticationService authService)
        {
            _googleUserRepository = googleUserRepository;
            _authService = authService;
        }

        [HttpGet("RegisterUser")]
        public async Task<IActionResult> RegisterUser(string acessToken)
        {
            try {
                var payload = await ValidateGoogleAccessTokenAsync(acessToken);

                var userExists = _googleUserRepository.GetUserByEmail(payload.Email);
                if (userExists != null) return Ok(await _authService.GenerateJwtToken(userExists));


                var user = new GoogleUser(payload.Email, payload.Name, payload.GivenName, payload.FamilyName, new GoogleRole(2, "Default"));
                _googleUserRepository.RegisterNewUser(user);

                return Ok(await _authService.GenerateJwtToken(user)); 
            }
            catch (Exception ex) {
                return Unauthorized($"Token invalido {ex.Message}");
            }
        }


        [HttpGet("GetUserToken")]
        public async Task<IActionResult> GetJwtUserTokenTest(string email)
        {
            var user = _googleUserRepository.GetUserByEmail(email);
            if (user == null) return NotFound();

            return Ok(await _authService.GenerateJwtToken(user));

        }


      
        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleAccessTokenAsync(string accessToken)
        {
           
                var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken);
                return payload;
        }



        

    }
}