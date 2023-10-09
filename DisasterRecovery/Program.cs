using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    option.LoginPath = "/Identity/Account/Login";
    option.LogoutPath = "/Identity/Account/Logout";
    option.AccessDeniedPath = "/Identity/Account/AccessDenied";
    option.SlidingExpiration = true;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("ContractorOnly", policy => policy.RequireClaim("Contractor"));
    options.AddPolicy("AnyUser",
        policy => policy.RequireAssertion(
            context => context.User.HasClaim(claim => claim.Type == "Admin")
            || context.User.HasClaim(claim => claim.Type == "Contractor")));
});

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
