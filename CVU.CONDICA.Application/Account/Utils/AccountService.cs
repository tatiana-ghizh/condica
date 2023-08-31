using CVU.CONDICA.Application.Security;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Security.Principal;

namespace CVU.CONDICA.Application.Account.Utils
{
    public static class AccountService
    {
        private const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits

        public static class Variables
        {
            public static readonly int PasswordValidity = 30;

            // Salt used to encrypt password
            public static readonly string Salt = "J2hrGsTUL~9+;QE~8ZZ5V";

            // Security code validity in seconds
            public static readonly double SecurityCodeValidity = TimeSpan.FromMinutes(5).TotalSeconds;

            public static readonly double CookieExpiration = TimeSpan.FromDays(1).TotalHours;
        }

        public static readonly List<string> ClaimsList = new()
        {
            Claims.UserId,
            Claims.UserRoles,
            Claims.RoleId,
            Claims.FullName,
            Claims.EmailAddress,
            Claims.PositionId,
            Claims.PositionName,
            //Claims.FirstName,
            //Claims.LastName,
        };

        public static CurrentUser GetAccountDetails(IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            var principal = accessor?.HttpContext?.User;

            var isAuthenticated = principal?.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated || principal is WindowsPrincipal)
            {
                return null;
            }

            var claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value);
            var options = serviceProvider.GetService(typeof(IOptions<TokenProviderOptions>)) as IOptions<TokenProviderOptions>;

            try
            {
                return new CurrentUser
                {
                    FullName = claims[Claims.FullName],
                    Role = (Role)int.Parse(claims[Claims.RoleId]),
                    Id = int.Parse(claims[Claims.UserId]),
                    Email = claims[Claims.EmailAddress],
                    //FirstName = claims[Claims.FirstName],
                    //LastName = claims[Claims.LastName],
                    //PositionId = int.Parse(claims[Claims.PositionId]),
                    //PositionName = claims[Claims.PositionName],
                };
            }
            catch
            {
                return new CurrentUser();
            }
        }

        public static Dictionary<string, string> GetClaims(User user)
        {
            var claims = new Dictionary<string, string>
        {
            { Claims.FullName, user.FirstName + " " + user.LastName },
            { Claims.UserRoles, user.Role.ToString() },
            { Claims.RoleId, ((int)user.Role).ToString() },
            { Claims.UserId, user.Id.ToString() },
            { Claims.EmailAddress, user.Email },
            //{ Claims.FirstName, user.FirstName },
            //{ Claims.LastName, user.LastName },
            //{ Claims.PositionId, ((int)user.PositionId).ToString() },
            //{ Claims.PositionName, user.Position.Name },
            { "IsActivated", user.IsActivated.ToString() }
        };

            return claims;
        }

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            // Produce a version 0 (see comment above) password hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) password hash.
            if (hashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, Pbkdf2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
