using CVU.CONDICA.Application.Account.Models;
using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static CVU.CONDICA.Application.Account.Utils.AccountService;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public LoginCommand(string email, string password, bool bearerAuth)
        {
            Email = email;
            Password = password;
            BearerAuth = bearerAuth;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool BearerAuth { get; set; }
    }

    public class LoginCommandHandler : RequestHandler<LoginCommand, LoginResult>
    {
        private readonly JwtService JwtService;

        public LoginCommandHandler(IServiceProvider serviceProvider, JwtService jwtService) : base(serviceProvider)
        {
            JwtService = jwtService;
        }

        public override async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = AppDbContext.User
                 .Include(x => x.Position)
                 .FirstOrDefault(d => d.Email == request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var details = Mapping.UserProjection.Compile().Invoke(user);

            var result = new LoginResult
            {
                User = details
            };

            var claims = GetClaims(user);

            result.Jwt = JwtService.GenerateToken(claims);

            return result;
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(IServiceProvider services)
        {
            RuleFor(d => d).Custom((obj, context) =>
            {
                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                if (db == null)
                {
                    context.AddFailure("Internal problem - needs admin attention.");
                    return;
                }

                var user = db.User.Where(d => d.Email == obj.Email.ToLower())
                               .Select(d => new { d.Email, d.IsBlocked, d.IsActivated, d.Password })
                               .FirstOrDefault();

                if (user == null)
                {
                    context.AddFailure("Invalid email address");
                    return;
                }

                var isCorrectPassword = VerifyHashedPassword(user.Password, AccountService.Variables.Salt + obj.Password);

                if (!isCorrectPassword)
                {
                    context.AddFailure("Invalid password.");
                    return;
                }

                if (user.IsBlocked)
                {
                    context.AddFailure("Your account is blocked.");
                }
            });
        }
    }
}
