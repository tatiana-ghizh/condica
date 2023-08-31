using CVU.CONDICA.Common.Extentions;
using MediatR;
using static CVU.CONDICA.Application.Account.Utils.AccountService;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class ResetPasswordSecurityCodeCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }

    public class ResetPasswordSecurityCodeCommandHandler : RequestHandler<ResetPasswordSecurityCodeCommand, Unit>
    {
        //private readonly IEmailService EmailService;

        public ResetPasswordSecurityCodeCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
           // EmailService = emailService;
        }

        public override async Task<Unit> Handle(ResetPasswordSecurityCodeCommand request, CancellationToken cancellationToken)
        {
            var user = AppDbContext.User.FirstOrDefault(d => d.Email == request.Email);
            if (user != null)
            {
                string clearPassword = StringExtensions.GenerateRandomPassword(8);

                user.Password = HashPassword(Variables.Salt + clearPassword);
                user.IsActivated = false;

                await AppDbContext.SaveChangesAsync(cancellationToken);

                //SendResetPasswordEmail(user.Email, user.FirstName, clearPassword);
            }

            return Unit.Value;
        }

        //private void SendResetPasswordEmail(string email, string firstName, string password)
        //{
        //    string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\EmailTemplates";
        //    var template = File.ReadAllText(assemblyPath + "\\ResetPassword.txt");

        //    template = template.Replace("{BaseUrl}", HostName)
        //        .Replace("{Password}", password)
        //        .Replace("{FirstName}", firstName);

        //    EmailService.QuickSendAsync(subject: "Reset Your Password", body: template, to: email);
        //}
    }
}
