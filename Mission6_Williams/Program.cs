using Microsoft.EntityFrameworkCore;
using Mission6_Williams.Models; // This connects to your MovieCollectionContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// CONFIGURATION: Connect to the SQLite Database
builder.Services.AddDbContext<MovieCollectionContext>(options =>
{
    // Make sure this matches the name in your appsettings.json
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieConnection"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();