using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TraversalCoreProject.Models; // CustomIdentityValidator burada

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Identity
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<CustomIdentityValidator>();

// GenericRepository için DI
builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));

// MVC + global authorize policy
builder.Services.AddControllersWithViews(cfg =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    cfg.Filters.Add(new AuthorizeFilter(policy));
});

// Cookie ayarları (Admin ve Member area’ya göre yönlendirme)
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

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
