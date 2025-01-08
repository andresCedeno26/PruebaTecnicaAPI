using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace PruebaTecnicaAPI.Util
{
    public class Jwt
    {
        public static string GenerateJwtToken<T>(string secretKey, T value)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("RequestPersonas", JsonConvert.SerializeObject(value)),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public static T ReadJwtToken<T>(string jwtToken, string secretKey, T value)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                // Lee y valida el token JWT
                jwtToken = jwtToken.Replace("Bearer ", "");
                var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out _);
                foreach (var item in principal.Claims)
                {
                    value = JsonConvert.DeserializeObject<T>(item.Value);
                    break;
                }
                return value;
            }
            catch (Exception ex)
            {
                // El token no es válido
                return value;
            }
        }
        public static bool ValidateJwtToken(string token, string secretKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secretKey);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Puedes configurar esto según tus necesidades
                    ValidateAudience = false, // Puedes configurar esto según tus necesidades
                    ValidateLifetime = true, // Validar la expiración del token
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero, // Asegura que no hay desviación de tiempo
                };
                token = token.Replace("Bearer ", "");
                // Intenta validar el token
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                // Si llega aquí sin lanzar una excepción, el token es válido y no ha expirado
                return true;
            }
            catch (Exception ex)
            {
                // Si hay un error al validar el token, o ha expirado o es inválido
                // Aquí puedes imprimir el error ex.Message para obtener más detalles
                return false;
            }
        }
    }
}
