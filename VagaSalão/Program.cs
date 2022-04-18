using Microsoft.EntityFrameworkCore;
using VagaSalão.Contexto;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<appContexto>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("appContexto")));

builder.Services.AddAuthentication("Identity.login")
    .AddCookie("Identity.login", config =>
    {
        config.Cookie.Name = "Identity.login";
        config.LoginPath = "/Login";
        config.AccessDeniedPath = "/Home";
        config.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
