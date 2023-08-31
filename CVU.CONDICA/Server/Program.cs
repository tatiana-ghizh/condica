using CVU.CONDICA.Application.Services.Infrastructure;
using CVU.CONDICA.ExceptionHandling.DependencyInjection;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Server.Config;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.ConfigureInjection(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddJwt(builder.Configuration);

//builder.Services.AddMudServices(options =>
//{
//    options.PopoverOptions.ThrowOnDuplicateProvider = false;
//});

var app = builder.Build();

ServiceActivator.Configure(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var appDbContext = services.GetRequiredService<AppDbContext>();

        appDbContext.SeedDb();
    }
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.AddGlobalErrorHandler();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
