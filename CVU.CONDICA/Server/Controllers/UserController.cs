using CVU.CONDICA.Application.Account.Commands;
using CVU.CONDICA.Application.Account.Queries;
using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.Application.Blobs.Commands;
using CVU.CONDICA.Application.Blobs.Queries;
using CVU.CONDICA.Application.Security;
using CVU.CONDICA.Dto.Blob;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.RequestModels;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Server.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly TokenProviderOptions tokenOptions;

        public UserController(IOptions<TokenProviderOptions> tokenOptions)
        {
            this.tokenOptions = tokenOptions.Value;
        }


        [HttpPost("log-in")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LoginDto model)
        {
            var command = new LoginCommand(model.Email, model.Password, model.BearerAuth);

            var result = await Mediator.Send(command);

            if (command.BearerAuth)
            {
                return Ok(new { Jwt = result.Jwt.Value });
            }

            var cookieOptions = new CookieOptions
            {
                MaxAge = result.Jwt.ExpiresIn,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append(result.Jwt.CookieName, result.Jwt.Value, cookieOptions);
            Response.Headers.Append(tokenOptions.ExpirationHeaderName, (tokenOptions.ExpirationSeconds * 1000).ToString());

            return Ok(result.User);
        }

        [HttpGet("log-out")]
        [HttpPost("log-out")]
        [AllowAnonymous]
        public IActionResult LogOut()
        {
            AuthHelper.RemoveUserKey();
            Response.Cookies.Delete(tokenOptions.CookieName);

            return Ok();
        }

        [HttpGet]
        public async Task<PaginatedModel<UserShortDto>> GetUsers([FromQuery] UserListQueryModel queryModel)
        {
            var query = new UserListQuery(queryModel);

            var mediatorResponse = await Mediator.Send(query);

            return mediatorResponse;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserDto> GetUser([FromRoute] int id)
        {
            return await Mediator.Send(new UserQuery(id));
        }

        [HttpPost]
        public async Task<int> CreateUser([FromBody] CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task EditUser([FromRoute] int id, [FromBody] EditUserDto model)
        {
            var command = new EditUserCommand(id, model);

            await Mediator.Send(command);
        }

        [HttpPost("change-password")]
        public async Task ChangePassword([FromBody] ChangePasswordDto model)
        {
            var command = new ChangePasswordCommand(model.CurrentPassword, model.NewPassword, model.ConfirmPassword);

            await Mediator.Send(command);
        }

        [HttpPost("reset-password-code")]
        public async Task SendResetPasswordCode([FromBody] ResetPasswordSecurityCodeCommand command)
        {
            await Mediator.Send(command);
        }

        [HttpPost("reset-password")]
        public async Task ResetPassword([FromBody] ResetPasswordDto model)
        {
            var command = new ResetPasswordCommand(model);

            await Mediator.Send(command);
        }

        [HttpGet("{id}/blob")]
        public async Task<IEnumerable<BlobDto>> GetBlobs([FromRoute] int id, [FromQuery] BlobQueryModel queryModel)
        {
            var query = new BlobListQuery(id, BlobComponent.User, queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("{id}/blob/download/{blobId}")]
        public async Task<byte[]> DownloadBlobs([FromRoute] int id, [FromRoute] int blobId)
        {
            var query = new DownloadBlobQuery(id, blobId);

            var result = await Mediator.Send(query);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return result.Content;
        }

        [HttpPost("{id}/blob")]
        public async Task UploadBlob([FromRoute] int id, [FromBody] CreateBlobDto model)
        {
            var command = new CreateBlobCommand(id, model.Content, model.Name, model.BlobType, BlobComponent.Article);

            await Mediator.Send(command);
        }

        [HttpDelete("{id}/blob/{blobId}")]
        public async Task DeleteBlob([FromRoute] int id, [FromRoute] int blobId)
        {
            var command = new DeleteBlobCommand(id, blobId, BlobComponent.Article);

            await Mediator.Send(command);
        }
    }
}
