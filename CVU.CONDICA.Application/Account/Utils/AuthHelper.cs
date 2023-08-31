using CVU.CONDICA.Application.Security;
using CVU.CONDICA.Application.Services.Infrastructure;
using CVU.CONDICA.Dto.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace CVU.CONDICA.Application.Account.Utils
{
    public static class AuthHelper
    {
        private static readonly ConcurrentDictionary<string, string> Keys;

        private static readonly string FilePath;
        private const string AesEncryptionKey = "F3777711E7B2C7A821BDB3B21C9A7945";
        private static readonly object LockObj = new();

        static AuthHelper()
        {
            Keys = new ConcurrentDictionary<string, string>();

            using var scope = ServiceActivator.GetScope();

            var dataPath = scope.ServiceProvider.GetService<IConfiguration>()?.GetSection("DataPath").Value;
            FilePath = Path.Combine(dataPath ?? ".", "user-keys.json");

            if (!File.Exists(FilePath)) return;

            using var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var textReader = new StreamReader(fileStream);

            var encryptedJson = textReader.ReadToEnd();
            var json = Decrypt(encryptedJson, AesEncryptionKey);

            JsonConvert.PopulateObject(json, Keys);
        }

        public static SymmetricSecurityKey GetUserKey(string email = null)
        {
            using var scope = ServiceActivator.GetScope();
            var tokenOptions = scope.ServiceProvider.GetService<IOptions<TokenProviderOptions>>().Value;

            if (email == null)
            {
                var accountDetails = scope.ServiceProvider.GetService<UserDto>();
                if (!accountDetails.IsAuthenticated)
                {
                    throw new ArgumentException($"{email}");
                }

                email = accountDetails.Email;
            }

            if (!Keys.TryGetValue(email, out var userKey))
            {
                userKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("+", string.Empty)
                    .Replace("=", string.Empty);

                Keys[email] = userKey;

                var json = JsonConvert.SerializeObject(Keys);
                var encryptedJson = Encrypt(json, AesEncryptionKey);

                var directoryPath = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                lock (LockObj)
                {
                    File.WriteAllText(FilePath, encryptedJson);
                }
            }

            var secretKey = tokenOptions.SecurityKey + userKey;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return securityKey;
        }

        public static void RemoveUserKey(string email = null)
        {
            using var scope = ServiceActivator.GetScope();
            if (email == null)
            {
                var accountDetails = scope.ServiceProvider.GetService<UserDto>();
                if (accountDetails == null || !accountDetails.IsAuthenticated)
                {
                    return;
                }

                email = accountDetails.Email;
            }

            Keys.TryRemove(email, out _);
        }

        public static string Encrypt(string text, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(text);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string text, string key)
        {
            var bytes = Convert.FromBase64String(text);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(bytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
