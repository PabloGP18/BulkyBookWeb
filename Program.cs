using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// here you can register example: Mail or DB
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Making connection with the connection string inside the appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// AddRazorRuntimeCompilation(); you need to add if you are not working with .net6, because of Hot reload it's not required anymore!
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
