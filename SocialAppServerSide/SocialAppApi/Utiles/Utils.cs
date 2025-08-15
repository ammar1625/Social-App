using System.IdentityModel.Tokens.Jwt;

namespace SocialAppApi.Utiles
{
    public static class Utils
    {
        public static bool TryGetTokenExpiry(string jwtToken, out DateTime expiry)
        {
            expiry = DateTime.MinValue;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(jwtToken))
                {
                    var token = handler.ReadJwtToken(jwtToken);
                    var expClaim = token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

                    if (long.TryParse(expClaim, out long expAsSeconds))
                    {
                        // Convert Unix timestamp (seconds since epoch) to DateTime
                        expiry = DateTimeOffset.FromUnixTimeSeconds(expAsSeconds).UtcDateTime;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing token: {ex.Message}");
            }

            return false;
        }
    }
}
