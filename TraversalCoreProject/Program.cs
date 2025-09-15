using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using TraversalCoreProject.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProject.Models;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION
var configuration = builder.Configuration;
var services = builder.Services;

// SERVICES
services.AddScoped<GetAllDestinationQueryHandler>();
services.AddScoped<GetDestinationByIDQueryHandler>();
services.AddScoped<CreateDestinationCommandHandler>();
services.AddScoped<RemoveDestinationCommandHandler>();
services.AddScoped<UpdateDestinationCommandHandler>();

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Debug);
    x.AddDebug();
});
var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<Context>(options=>
options.UseSqlServer(connectionString));

services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
    

services.AddHttpClient();

services.ContainerDependencies();

services.AddAutoMapper(typeof(Program));

services.CustomerValidator();

services.AddControllersWithViews();
services.AddFluentValidationAutoValidation();
services.AddFluentValidationClientsideAdapters();


services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/SignIn/";
});

// BUILD
var app = builder.Build();

// LOGGER
var path = Directory.GetCurrentDirectory();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");

// MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

var suppertedCultures = new[] { "en", "fr", "es", "tr", "de" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(suppertedCultures[1])
    .AddSupportedCultures(suppertedCultures)
    .AddSupportedUICultures(suppertedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
