using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CVU.CONDICA.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorage;

        public ServerAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //await localStorage.SetItemAsStringAsync("token", "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.eyJmdWxsTmFtZSI6IkN2dSBUZW1wbGF0ZSIsInVzZXJSb2xlcyI6IkFkbWluaXN0cmF0b3IiLCJyb2xlSWQiOiIxIiwidXNlcklkIjoiMSIsImVtYWlsQWRkcmVzcyI6ImN2dS50ZW1wbGF0ZUBjdnUucm8iLCJGaXJzdE5hbWUiOiJDdnUiLCJMYXN0TmFtZSI6IlRlbXBsYXRlIiwibmJmIjoxNjYwMjQxMjIxLCJleHAiOjE2NjAyNTAyMjEsImlzcyI6InRlbXBsYXRlc2VydmVyIiwiYXVkIjoidGVtcGxhdGVjbGllbnQifQ.");

            var token = await localStorage.GetItemAsync<string>("token");

            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            var decoder = new JwtDecoder(token, true);

            var claims = decoder._payload.Select(d => new Claim(d.Key.ToString(), d.Value.ToString())).ToList();


            var claimIdentity = new List<ClaimsIdentity>() { new ClaimsIdentity(claims, "JWT", "emailAdress", "userRoles") };

            return new AuthenticationState(new ClaimsPrincipal(claimIdentity));
        }

        private static IEnumerable<Claim> EnumerateClaims(JsonElement json)
        {
            foreach (var claim in json.EnumerateObject())
            {
                if (claim.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var value in claim.Value.EnumerateArray())
                    {
                        yield return new Claim(claim.Name, value.ToString());
                    }
                }
                else
                {
                    yield return new Claim(claim.Name, claim.Value.ToString());
                }
            }
        }
    }

    public static class StringExtensions
    {
        public static int GetNextHighestMultiple(this int source, int multipicand)
        {
            int result = source;
            while ((result % multipicand) != 0)
            {
                result++;
            }
            return result;
        }
    }

    // Shamelessly ripped from the MS implementation :-)
    public struct JwtRegisteredClaimNames
    {
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Actort = "actort";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Typ = "typ";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Sub = "sub";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-frontchannel-1_0.html#OPLogout
        public const string Sid = "sid";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Prn = "prn";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Nbf = "nbf";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Nonce = "nonce";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string NameId = "nameid";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Jti = "jti";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Iss = "iss";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Iat = "iat";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string GivenName = "given_name";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string FamilyName = "family_name";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Gender = "gender";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Exp = "exp";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Email = "email";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-core-1_0.html#CodeIDToken
        public const string AtHash = "at_hash";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string CHash = "c_hash";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Birthdate = "birthdate";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Azp = "azp";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string AuthTime = "auth_time";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Aud = "aud";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Amr = "amr";
        //
        // Summary:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Acr = "acr";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string UniqueName = "unique_name";
        //
        // Summary:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Website = "website";

        //
        // Summary:
        //     Microsoft have to be different, if you want to use roles for auth in the API, you have to use this
        //     rather than the ietf standard "Role"..... (Added by shawty, as not in official JWT constants list)
        public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    }

    public class JwtDecoder
    {
        private Dictionary<string, object> _header;
        public Dictionary<string, object> _payload;
        private string _token = String.Empty;

        public JwtDecoder(string token, bool autoDecode = false)
        {
            _token = token;
            if (autoDecode) Decode();
        }

        public void Decode()
        {
            string[] parts = _token.Split('.');
            if (parts.Length != 3) throw new Exception("Malformed JWT token");

            string b64Header = parts[0];
            string b64Payload = parts[1];

            // B64 strings must be a length that is a multiple of 4 to decode them, so ensure this before we try to use them
            if (b64Header.Length % 4 != 0)
            {
                var lengthToBe = b64Header.Length.GetNextHighestMultiple(4);
                b64Header = b64Header.PadRight(lengthToBe, '=');
            }

            if (b64Payload.Length % 4 != 0)
            {
                var lengthToBe = b64Payload.Length.GetNextHighestMultiple(4);
                b64Payload = b64Payload.PadRight(lengthToBe, '=');
            }

            string headerJson = Encoding.UTF8.GetString(Convert.FromBase64String(b64Header));
            string payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(b64Payload));

            _header = JsonConvert.DeserializeObject<Dictionary<string, object>>(headerJson);
            _payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(payloadJson);

        }

        public object GetKey(string keyName)
        {
            if (!_payload.ContainsKey(keyName)) throw new Exception($"JWT Token does not contain key {keyName}");
            return _payload[keyName];
        }
    }
}
