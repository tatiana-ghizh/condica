using CVU.CONDICA.Application.Interfaces;
using CVU.CONDICA.Application.Services.UserSecurityCodeGenerator;
using CVU.CONDICA.Common.Extentions;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Persistence.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using static CVU.CONDICA.Application.Account.Utils.AccountService;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LodgeId { get; set; }
        public int? CandidateId { get; set; }

        public static User Create(RegisterCommand model)
        {
            return new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = HashPassword(Variables.Salt + model.Password),
                Role = Role.Member,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                IsActivated = false,
            };
        }
    }

    public class RegisterCommandHandler : RequestHandler<RegisterCommand, int>
    {
        //private readonly IEmailService EmailService;
        private readonly IUserSecurityCodeGeneratorService _userSecurityCodeGeneratorService;

        public RegisterCommandHandler(IServiceProvider serviceProvider, IUserSecurityCodeGeneratorService userSecurityCodeGeneratorService) : base(serviceProvider)
        {
            //EmailService = emailService;
            _userSecurityCodeGeneratorService = userSecurityCodeGeneratorService;
        }

        public override async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = RegisterCommand.Create(request);

            AppDbContext.Add(user);
            await AppDbContext.SaveChangesAsync(cancellationToken);

           // SendRegistrationEmail(user.Email, user.FirstName, request.Password);

            return user.Id;
        }

        //private void SendRegistrationEmail(string userEmail, string firstName, string password)
        //{
        //    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\EmailTemplates";
        //    var template = File.ReadAllText(assemblyPath + "\\UserRegister.txt");

        //    template = template.Replace("{BaseUrl}", HostName)
        //        .Replace("{Password}", password)
        //        .Replace("{FirstName}", firstName);

        //    EmailService.QuickSendAsync(
        //        subject: "Bun venit in RSAA!",
        //        body: template,
        //        to: userEmail);
        //}
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(IServiceProvider services)
        {
            RuleFor(d => d).Custom((obj, context) =>
            {
                if (!StringExtensions.ValidatePassword(obj.Password))
                {
                    context.AddFailure("Password requirements are not met.");
                }

                if (string.IsNullOrEmpty(obj.Email))
                {
                    context.AddFailure("Email is required");
                    return;
                }

                if (!Regex.IsMatch(obj.Email, @"[a-z0-9!#$%&'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
                {
                    context.AddFailure("Invalid email address format");
                }

                if (string.IsNullOrEmpty(obj.FirstName) || string.IsNullOrEmpty(obj.LastName))
                {
                    context.AddFailure("First name and Last name are required");
                    return;
                }

                if (obj.FirstName.Length > 50 || obj.LastName.Length > 50)
                {
                    context.AddFailure("First Name and Last Name has a maximum length of 50");
                }

                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                if (db == null)
                {
                    context.AddFailure("Internal problem - needs admin attention.");
                    return;
                }

                var emailExists = db.User.Any(d => d.Email == obj.Email.ToLower());

                if (emailExists)
                {
                    context.AddFailure("There is already an account associated to this email");
                }
            });
        }
    }
}
