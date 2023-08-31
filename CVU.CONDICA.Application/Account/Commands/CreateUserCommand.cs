using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.Application.Services.UserSecurityCodeGenerator;
using CVU.CONDICA.Common.Extentions;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Persistence.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public bool IsBlocked { get; set; }

        public static User Create(CreateUserCommand command, string password, string securityCode)
        {
            return new User
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PositionId = command.PositionId,
                IsActivated = true,
                Password = AccountService.HashPassword(AccountService.Variables.Salt + password),
                IsBlocked = command.IsBlocked,
                SecurityCode = securityCode,
                Role = Role.Administrator,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }
    }

    public class CreateUserCommandHandler : RequestHandler<CreateUserCommand, int>
    {
        //private readonly IEmailService EmailService;
        private readonly IUserSecurityCodeGeneratorService _userSecurityCodeGeneratorService;

        public CreateUserCommandHandler(IServiceProvider serviceProvider, IUserSecurityCodeGeneratorService userSecurityCodeGeneratorService) : base(serviceProvider)
        {
            //EmailService = emailService;
            _userSecurityCodeGeneratorService = userSecurityCodeGeneratorService;
        }

        public override async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var password = StringExtensions.GenerateRandomPassword(12);

            var securityCode = _userSecurityCodeGeneratorService.Next().ToString();

            var newUser = CreateUserCommand.Create(request, password, securityCode);

            AppDbContext.User.Add(newUser);
            await AppDbContext.SaveChangesAsync(cancellationToken);

            //SendRegistrationEmail(newUser.Email, newUser.FirstName, password);

            return newUser.Id;
        }

        //private void SendRegistrationEmail(string userEmail, string firstName, string password)
        //{
        //    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\EmailTemplates";
        //    var template = File.ReadAllText(assemblyPath + "\\NewUserCreated.txt");

        //    template = template.Replace("{BaseUrl}", HostName)
        //        .Replace("{Password}", password)
        //        .Replace("{FirstName}", firstName);

        //    EmailService.QuickSendAsync("Welcome", template, userEmail);
        //}
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IServiceProvider services)
        {
            RuleFor(d => d).Custom((obj, context) =>
            {
                if (!new List<int> { (int)Role.Administrator, (int)Role.Member }.Contains(obj.RoleId))
                {
                    context.AddFailure("Invalid Role.");
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
                    context.AddFailure("there is already an account associated to this email");
                }
            });
        }
    }
}
