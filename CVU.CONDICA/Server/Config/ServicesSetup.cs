using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Application.Services.Infrastructure;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CVU.CONDICA.Application.Security;
using CVU.CONDICA.Application.Account.Utils;
using CVU.CONDICA.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using CVU.CONDICA.Application;
using ISession = CVU.CONDICA.Application.Interfaces.ISession;
using FluentValidation.AspNetCore;
using CVU.CONDICA.Application.Account.Commands;
using CVU.CONDICA.Application.Services.UserSecurityCodeGenerator;

namespace CVU.CONDICA.Server.Config
{
    public static class ServicesSetup
    {
        public static void ConfigureInjection(this IServiceCollection services, ConfigurationManager Configuration)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default"),
                                                   b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            //services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddCors(
                o => o.AddPolicy("TEMPLATE_CORS_POLICY",
                b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IUserSecurityCodeGeneratorService, UserSecurityCodeGeneratorService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<ISession, Session>();
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = new TokenProviderOptions();
            configuration.GetSection("TokenProvider").Bind(tokenOptions);

            services.Configure<TokenProviderOptions>(configuration.GetSection("TokenProvider"));

            services.AddScoped<JwtService>();
            services.AddScoped(AccountService.GetAccountDetails);

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireClaim("roleId", ((int)Role.Administrator).ToString()));
                options.AddPolicy("Member", policy => policy.RequireClaim("roleId", ((int)Role.Member).ToString()));
                options.AddPolicy("AccountActivated", policy => policy.RequireClaim("IsActivated", "True"));
            })
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = Claims.EmailAddress,
                        RoleClaimType = Claims.UserRoles,

                        ValidateIssuer = true,
                        ValidIssuer = tokenOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = tokenOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeyResolver = (_, st, kid, vp) =>
                        {
                            var claims = ((System.IdentityModel.Tokens.Jwt.JwtSecurityToken)st).Claims;

                            var userName = claims.FirstOrDefault(c => c.Type == Claims.EmailAddress);
                            return userName == null ? Array.Empty<SymmetricSecurityKey>() : new[] { AuthHelper.GetUserKey(userName.Value) };
                        },

                        ValidateLifetime = true,
                        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                        {
                            return notBefore <= DateTime.UtcNow && expires >= DateTime.UtcNow;
                        },
                        AuthenticationType = JwtBearerDefaults.AuthenticationScheme
                    };

                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[tokenOptions.CookieName] ??
                                (string)context.Request.HttpContext.Items[tokenOptions.CookieName];

                            if (string.IsNullOrEmpty(context.Token))
                            {
                                context.Request.Headers.TryGetValue("Authorization", out var bearerToken);
                                context.Token = bearerToken.ToString().Replace("bearer ", "");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services
            .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilter)));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors();
            services.AddOptions();
            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RSAA API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }

                });
            });

            services
              .AddControllers(o =>
              {
                  o.RespectBrowserAcceptHeader = true;
                  o.ReturnHttpNotAcceptable = true;
              })
              .AddFluentValidation(o =>
              {
                  o.RegisterValidatorsFromAssemblyContaining<LoginCommandValidator>();
              });

            services.AddMediatR(typeof(RegisterCommandHandler).Assembly);
        }
    }
}
