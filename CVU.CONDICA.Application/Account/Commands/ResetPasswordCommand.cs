using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.Common.Extentions;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public ResetPasswordCommand(ResetPasswordDto model)
        {
            Model = model;
        }

        public ResetPasswordDto Model { get; set; }
    }

    public class ResetPasswordCommandHandler : RequestHandler<ResetPasswordCommand, Unit>
    {

        public ResetPasswordCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var administrationUser = AppDbContext.User.First(f => f.SecurityCode == model.SecurityCode);

            var newPassword = AccountService.HashPassword(AccountService.Variables.Salt + model.Password);

            administrationUser.Password = newPassword;
            administrationUser.SecurityCodeExpiresAt = null;
            administrationUser.SecurityCode = null;

            await AppDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator(IServiceProvider services)
        {
            RuleFor(d => d).Custom((obj, context) =>
            {
                var model = obj.Model;

                if (!StringExtensions.ValidatePassword(model.Password))
                {
                    context.AddFailure("Password requirements are not met.");
                }
                if (model.Password != model.ConfirmPassword)
                {
                    context.AddFailure("Password and confirm password are not equal");
                }

                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                if (db == null)
                {
                    context.AddFailure("Internal problem - needs admin attention.");
                    return;
                }

                var securityCodeExpiration = db.User
                                            .Where(d => d.SecurityCode == model.SecurityCode)
                                            .Select(d => d.SecurityCodeExpiresAt)
                                            .FirstOrDefault();

                if (securityCodeExpiration == null)
                {
                    context.AddFailure("Invalid Security Code");
                }
                else if (securityCodeExpiration.Value < DateTime.UtcNow)
                {
                    context.AddFailure("Reset code expired. Please make a new request.");
                }
            });
        }
    }
}
