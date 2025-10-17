using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UMS.Domain.Entities;

namespace UMS.Application.Helper
{
    public static class HelperUtility
    {
        public static string GenerateJwtToken(AuthenticationResponse user, IConfiguration _config)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Password(string password)
        {
            string _pwd =
            Convert.ToBase64String(
                BCrypt.Generate(
                    BCrypt.PasswordToByteArray(password.ToCharArray()),
                    new byte[16], // You should use a proper salt here
                    10 // Cost factor
                )
            );
            return _pwd;
        }
    }
}
