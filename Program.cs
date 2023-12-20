using Loginpage.Models.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var provider = builder.Services.BuildServiceProvider();
var Config = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<ApplicationContext>(item => item.UseNpgsql(Config.GetConnectionString("Conn"))); 
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Login/LoginPage";
    option.LoginPath = "/Login/LoginPage";
    option.LogoutPath = "/Login/Logout";
    option.Cookie.Name = "Authweb";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
