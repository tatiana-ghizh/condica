using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ISession = CVU.CONDICA.Application.Interfaces.ISession;

namespace CVU.CONDICA.Application
{
    public abstract class RequestHandler<TRequest, TResponse> : Session, IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected AppDbContext AppDbContext;

        protected RequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            AppDbContext = (AppDbContext)serviceProvider.GetService(typeof(AppDbContext));
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public class Session : ISession
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public string HostName { get; set; }

        private IConfiguration Configuration { get; }

        public Session(IServiceProvider serviceProvider)
        {
            Configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));
            HttpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));

            CurrentUser = AccountService.GetAccountDetails(serviceProvider);

            HostName = Configuration.GetSection("HostName").Value;
        }
    }
}
