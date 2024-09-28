using System.IdentityModel.Tokens.Jwt;

namespace DoubleV.Helpers
{
    public static class GetRoleFromToken
    {
        public static string Get(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);                       
            
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");                        

            return roleClaim?.Value;
        }
    }
}
