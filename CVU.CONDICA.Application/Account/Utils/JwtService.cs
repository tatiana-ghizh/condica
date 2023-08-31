using CVU.CONDICA.Application.Account.Models;
using CVU.CONDICA.Application.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CVU.CONDICA.Application.Account.Utils
{
    public class JwtService
    {
        private readonly TokenProviderOptions tokenOptions;

        public JwtService(IOptions<TokenProviderOptions> tokenOptions)
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public TokenDto GenerateToken(IDictionary<string, string> data)
        {
            var claims = new List<Claim>();

            foreach (var (key, value) in data ?? Enumerable.Empty<KeyValuePair<string, string>>())
            {
                claims.Add(new Claim(key, value ?? string.Empty));
            }

            return Generate(tokenOptions, claims);
        }

        public static TokenDto Generate(TokenProviderOptions tokenOptions, List<Claim> claims)
        {
            var userName = claims.FirstOrDefault(q => q.Type == "emailAddress").Value;

            var jwt = new TokenDto
            {
                ExpiresIn = TimeSpan.FromSeconds(tokenOptions.ExpirationSeconds),
                CookieName = tokenOptions.CookieName
            };

            var securityKey = AuthHelper.GetUserKey(userName);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.UtcNow.AddSeconds(tokenOptions.ExpirationSeconds),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            jwt.Value = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return jwt;
        }
    }
}
