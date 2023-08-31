using CVU.CONDICA.Dto.UserManagement;
using Microsoft.AspNetCore.Http;

namespace CVU.CONDICA.Application.Interfaces
{
    public interface ISession
    {
        CurrentUser CurrentUser { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}
