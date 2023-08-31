using Blazored.LocalStorage;
using CVU.CONDICA.Client;
using CVU.CONDICA.Client.Services;
using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Positions;
using CVU.CONDICA.Dto.RequestModels;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Dto.Vacations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddTransient<DialogsService>();
builder.Services.AddScoped<CurrentUserService>();

//register Pagination Service
builder.Services.AddTransient<PaginationManager<UserShortDto, UserListQueryModel>>();
builder.Services.AddTransient<PaginationManager<CompanyProjectDto, CompanyProjectListQueryDto>>();
builder.Services.AddTransient<PaginationManager<PositionDto, PositionListQueryDto>>();
builder.Services.AddTransient<PaginationManager<VacationDto, VacationListQueryDto>>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireClaim("roleId", ((int)Role.Administrator).ToString()));
    options.AddPolicy("Member", policy => policy.RequireClaim("roleId", ((int)Role.Member).ToString()));
    options.AddPolicy("AccountActivated", policy => policy.RequireClaim("IsActivated", "True"));
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<BlazorHttpClient>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices(config => { config.PopoverOptions.ThrowOnDuplicateProvider = false; });
//builder.Services.AddMudServices(options =>
//{
//    options.PopoverOptions.ThrowOnDuplicateProvider = false;
//});

await builder.Build().RunAsync();
