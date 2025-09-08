using BusinessLayer.Container;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using DTOLayer.DTOs.AnnouncementDTOs;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.IO;
using TraversalCoreProject.Models; // Bunu da kaldırabilirsin eğer Models namespace yoksa

var builder = WebApplication.CreateBuilder(args);

// 🔹 Logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddDebug();

// 🔹 DbContext
builder.Services.AddDbContext<Context>();

// 🔹 Identity
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>()
    .AddDefaultTokenProviders();

// 🔹 GenericRepository DI
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));

builder.Services.AddHttpClient();
// 🔹 Business & DataAccess bağımlılıkları
builder.Services.ContainerDependencies();

// 🔹 AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.CustomerValidator();

// 🔹 MVC + global authorize policy
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddMvc(cfg =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    cfg.Filters.Add(new AuthorizeFilter(policy));
});

// 🔹 Cookie ayarları
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/Login/AccessDenied";

    opt.Events.OnRedirectToLogin = context =>
    {
        var path = context.Request.Path;

        if (path.StartsWithSegments("/Admin"))
        {
            context.Response.Redirect("/Admin/Login/Index");
        }
        else if (path.StartsWithSegments("/Member"))
        {
            context.Response.Redirect("/Member/Login/Index");
        }
        else
        {
            context.Response.Redirect("/Login/Index");
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

// 🔹 Middleware pipeline
var path = Directory.GetCurrentDirectory();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { "en", "fr", "es", "gr", "tr", "de" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

// 🔹 Route tanımları
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
