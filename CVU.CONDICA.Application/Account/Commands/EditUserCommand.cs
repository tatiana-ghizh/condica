using CVU.CONDICA.Application.Interfaces;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Persistence.Context;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.CONDICA.Application.Account.Commands
{
    public class EditUserCommand : IRequest<Unit>
    {
        public EditUserCommand()
        {
        }

        public EditUserCommand(int id, EditUserDto postModel)
        {
            Id = id;
            FirstName = postModel.FirstName;
            LastName = postModel.LastName;
            Role = postModel.Role;
            IsBlocked = postModel.IsBlocked;
            Role = postModel.Role;
        }

        internal int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role? Role { get; set; }
        public bool? IsBlocked { get; set; }
    }

    public class EditUserCommandHandler : RequestHandler<EditUserCommand, Unit>
    {
        public EditUserCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                request.Id = CurrentUser.Id;
            }

            var administrationUser = AppDbContext.User
                .First(d => d.Id == request.Id);

            if (request.IsBlocked.HasValue)
            {
                administrationUser.IsBlocked = request.IsBlocked.Value;
            }
            if (request.Role.HasValue)
            {
                administrationUser.Role = request.Role.Value;
            }
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                administrationUser.FirstName = request.FirstName;
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                administrationUser.LastName = request.LastName;
            }

            await AppDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {

        public EditUserCommandValidator(IServiceProvider services, ISession session)
        {

            RuleFor(d => d).Custom((obj, context) =>
            {
                //context.AddFailure("Test exception!");

                if ((obj.Id == 0 || obj.Id == session.CurrentUser.Id) && (obj.Role.HasValue || obj.IsBlocked.HasValue))
                {
                    context.AddFailure("You cannot block yourself and you can't change your role");
                }

                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetService<AppDbContext>();

                if (db == null)
                {
                    context.AddFailure("Internal problem - needs admin attention.");
                    return;
                }

                var userId = obj.Id == 0 ? session.CurrentUser.Id : obj.Id;

                var userExists = db.User.Any(d => d.Id == userId);

                if (!userExists)
                {
                    context.AddFailure("Invalid User Id");
                }

                if (!string.IsNullOrEmpty(obj.LastName) && obj.LastName.Length > 50)
                {
                    context.AddFailure("Last Name has a maximum length of 50");
                }

                if (!string.IsNullOrEmpty(obj.FirstName) && obj.FirstName.Length > 50)
                {
                    context.AddFailure("First Name has a maximum length of 50");
                }
            });
        }
    }
}
