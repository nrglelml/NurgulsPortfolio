using BusinessLayer.Container;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddErrorDescriber<CustomIdentityValidator>();

builder.Services.AddHttpClient();
//businesslayer/container/extension.cs dosyasý için
builder.Services.ContainerDependencies();
//mapping iþlemi için
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMvc(config =>
{
    // Global olarak giriþ zorunluluðu
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Login/SignIn";
    options.LogoutPath = "/Admin/Login/LogOut";
    options.AccessDeniedPath = "/ErrorPage/AccessDenied";
});


var app = builder.Build();
app.SeedAdminUser();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public static class SeedExtensions
{
    public static void SeedAdminUser(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

        if (!roleManager.RoleExistsAsync("Admin").Result)
            roleManager.CreateAsync(new AppRole { Name = "Admin" }).Wait();

        if (userManager.FindByNameAsync("admin").Result == null)
        {
            var user = new AppUser
            {
                UserName = "admin",
                Email = "admin@portfolio.com",
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(user, "Admin123!").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}
