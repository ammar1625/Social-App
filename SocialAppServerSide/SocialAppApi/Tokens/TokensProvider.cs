using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialAppBusinessLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SocialAppApi.Tokens
{
    public class TokensProvider
    {
        
        public static string GenerateEmailConfirmationToken(IConfiguration _configuration, string email)
        {
            // Create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Get secret key from configuration
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            //Convert.FromBase64String(_configuration.GetValue<string>("JWT"));
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Create claims identity
            var claims = new[]
            {

            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Configure token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24), // Token expiration (24 hours)
                SigningCredentials = credentials,

            };

            // Generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static int GenerateOtpCode()
        {
            Random random = new Random();
            return random.Next(100000, 1000000); // Generates a number between 100000 and 999999
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32]; // 256 bits
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        public static string GenerateLogInAuthenticationtoken(IConfiguration _configuration, clsUser user)
        {
            //get the key
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            int JwtValidityTime = Convert.ToInt32(_configuration["JWT:JwtValidityTime"]);

            var TokenHandler = new JwtSecurityTokenHandler();

            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                //Issuer = _configuration.GetValue<string>("issuer"),
                //Audience = _configuration.GetValue<string>("audience"),
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name , user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.DateOfBirth,user.DateOfBirth.ToString()),
                        new Claim(ClaimTypes.MobilePhone , user.Phone)

                }),
                Expires = DateTime.Now.AddMinutes(JwtValidityTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };


            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);
        }

    }
}
