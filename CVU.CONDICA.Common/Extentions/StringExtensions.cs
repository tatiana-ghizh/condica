using System.Text.RegularExpressions;

namespace CVU.CONDICA.Common.Extentions
{
    public static class StringExtensions
    {
        public static bool ValidatePassword(string password)
        {
            return Regex.IsMatch(password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-+~()_]).{8,}$");
        }

        public static string SplitCamelCase(this string inputString)
        {
            return inputString.Aggregate(string.Empty, (result, next) =>
            {
                if (char.IsUpper(next) && result.Length > 0)
                {
                    result += ' ';
                }
                return result + next;
            });
        }

        public static string GenerateRandomPassword(int length)
        {
            var random = new Random();

            string letters = "abcdefghijklmnopqrstuvwxyz";
            string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "1234567890";
            string specialChar = "!@#$%^&";


            var pass1 = new string(Enumerable.Repeat(capitalLetters, 2).Select(s => s[random.Next(s.Length)]).ToArray());
            var pass2 = new string(Enumerable.Repeat(letters, 2).Select(s => s[random.Next(s.Length)]).ToArray());
            var pass3 = new string(Enumerable.Repeat(digits, 2).Select(s => s[random.Next(s.Length)]).ToArray());
            var pass4 = new string(Enumerable.Repeat(specialChar, 2).Select(s => s[random.Next(s.Length)]).ToArray());


            return pass1 + pass2 + pass3 + pass4;
        }

        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToUpperInvariant(a[0]);

            return new string(a);
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            char[] a = value.ToCharArray();
            a[0] = char.ToLowerInvariant(a[0]);

            return new string(a);
        }

    }
}
