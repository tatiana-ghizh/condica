namespace CVU.CONDICA.Application.Account.Models
{
    public class TokenDto
    {
        public string Value { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public string CookieName { get; set; }
    }
}
