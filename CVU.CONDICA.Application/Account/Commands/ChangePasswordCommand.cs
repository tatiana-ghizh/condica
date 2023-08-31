using CVU.CONDICA.Application.Interfaces;
using CVU.CONDICA.Common.Extentions;
using CVU.CONDICA.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static CVU.CONDICA.Application.Account.Utils.AccountService;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public ChangePasswordCommand(string currentPassword, string newPassword, string confirmPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : RequestHandler<ChangePasswordCommand, Unit>
    {
        public ChangePasswordCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = AppDbContext.User.First(d => d.Id == CurrentUser.Id);

            user.Password = HashPassword(Variables.Salt + request.NewPassword);
            user.IsActivated = true;

            await AppDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {

        public ChangePasswordCommandValidator(IServiceProvider services, ISession session)
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

                var user = db.User.FirstOrDefault(d => d.Id == session.CurrentUser.Id);

                if (user == null)
                {
                    context.AddFailure("You must be logged in.");
                    return;
                }

                var isCorrectPassword = VerifyHashedPassword(user.Password, Variables.Salt + obj.CurrentPassword);

                if (!isCorrectPassword)
                {
                    context.AddFailure("Invalid current password.");
                    return;
                }

                if (!StringExtensions.ValidatePassword(obj.NewPassword))
                {
                    context.AddFailure("Password requirements are not met.");
                }

                if (obj.NewPassword != obj.ConfirmPassword)
                {
                    context.AddFailure("Password and confirmation password doesn't match.");
                }

            });
        }
    }
}
