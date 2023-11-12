using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Services.Email;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);

builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => 
		options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddSingleton(typeof(IEmailSender<>), typeof(EmailSender<>));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("BetaRelease", new OpenApiInfo
    {
        Version = "Beta Release",
        Title = "Identity MVC",
        Description = "The following APIs are available:",
        License = new OpenApiLicense
        {
            Name = "License Copyright: MIT",
            Url = new Uri("https://github.com/sergiocbueno/identity-mvc/blob/master/LICENSE")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/BetaRelease/swagger.json", "BetaRelease");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
