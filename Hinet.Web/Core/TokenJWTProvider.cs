using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Microsoft.IdentityModel.Tokens;

namespace Hinet.Web.Core
{
    public class TokenJWTProvider
    {
        private static string privateKey = "sX5gSRdpBGkjK4GBPg4EVVuHkeuz2UuEPSBBuX4QXCVq68aFzCSUKAptEQV9ZHVYWa6DGrmzMPQFvYF8hNJySLMQ6UKyKE6mdYxwE2cKXRZb7naxQMt4cJHMBFsd4BKf3r7Zw6QmFa3attCRuWKChg2y9KYLMJm53vrTedaPcGq5QDp2cUVLwmZ35Jsq4k3p5Ahjvzueqmp3NtAL2QgELeQScCXfEeF3XY9b5smcFVNsEDJJgCLKuaeyfNQXRjnG";
        public static string Issuer = "HINET";
        public static string Audien = "OnlineGovSYS";
        public static Task<ClaimsIdentity> CreateClaimsIdentitiesAsync(AppUser user)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            // This next one is kinda of special. This lets you put non-convential JWT data in here in the format you desire.
            // DO NOT ABUSE IT! If your tokens get too fat you aren't using them as intended (as identity). You
            // will only hurt yourself in the long run.
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)));
            // Keep the UserData field small, such as just Ids that can then be used to look up data you need.
            // Instead of getting just a UserId and having to look up their LibraryId, you can  go straight
            // to using a LibraryId stored in the user data to look up books checked out.
            // It accepts any string as value, so it could be json, or csv, or tsv, etc. I chose a Json string for this
            // demo.

            // TODO: Roles. You need to connect to your database here to get the ACTUAL roles that you may
            // already be using. Some common examples are Security.UserRoles or dbo.UserRoles. Wherever your
            // roles are, you need to do that. Your schema is up for you to figure out or even create.
            // ex.) var roles = await _userRepository.GetRolesAsync(userId);
            // Use the Role model as an example of the data you need to acquire.
            // Roles could be Student, Employee, Trainer, Admin, etc.
            var roles = Enumerable.Empty<Role>(); // Not a real list.

            //foreach (var role in roles)
            //{ claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName)); }

            return Task.FromResult(claimsIdentity);
        }
        public static async Task<string> CreateJWTAsync(
                AppUser user,
                string issuer,
                string authority,
                int hoursValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = await CreateClaimsIdentitiesAsync(user);

            // Create JWToken
            var token = tokenHandler.CreateJwtSecurityToken(issuer: issuer,
                audience: authority,
                subject: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(hoursValid),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(privateKey)),
                        SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }

        public static JwtSecurityToken ReadJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var checkValid = ValidateToken(token);
            return tokenS;
        }
        public static (bool, string) ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audien,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(privateKey))
            };

            SecurityToken validatedToken;
            try
            {
                var rs = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (validatedToken != null, string.Empty);
        }
    }
}