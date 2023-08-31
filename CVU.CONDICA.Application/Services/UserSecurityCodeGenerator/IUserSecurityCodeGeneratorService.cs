namespace CVU.CONDICA.Application.Services.UserSecurityCodeGenerator
{
    public interface IUserSecurityCodeGeneratorService
    {
        Task<string> Next();
    }
}
